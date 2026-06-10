using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Order;
using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly INotificationService _notificationService;

    public OrderService(AppDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    private bool CanMove(OrderStatus current, OrderStatus next)
    {
        return current switch
        {
            OrderStatus.Pending => next == OrderStatus.Accepted,
            OrderStatus.Accepted => next == OrderStatus.Shipped,
            OrderStatus.Shipped => next == OrderStatus.Delivered,
            OrderStatus.Delivered => false,
            OrderStatus.Cancelled => false,
            _ => false
        };
    }

    public async Task<List<OrderAdminDto>> GetAllAsync(string lang, string? status, int page, int pageSize)
    {
        var query = _context.Orders
            .Include(x => x.User)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status) &&
            Enum.TryParse<OrderStatus>(status, true, out var parsed))
            query = query.Where(x => x.Status == parsed);

        var orders = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return orders.Select(x => new OrderAdminDto
        {
            Id = x.Id,
            UserFullName = x.User.FullName,
            UserEmail = x.User.Email,
            TotalPrice = x.TotalPrice,
            Status = x.Status.ToString(),
            RejectReason = x.RejectReason,
            CreatedAt = x.CreatedAt,
            Items = x.OrderItems.Select(i => new OrderItemGetDto
            {
                ProductName = i.Product.NameAz,
                ProductImageUrl = i.Product.ImageUrl,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        }).ToList();
    }

    public async Task CancelOrderAsync(int userId, int orderId)
    {
        var order = await _context.Orders
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == orderId && x.UserId == userId)
            ?? throw new KeyNotFoundException("Sifariş tapılmadı.");

        if (order.Status != OrderStatus.Pending)
            throw new InvalidOperationException("Yalnız gözləmədə olan sifarişlər ləğv edilə bilər.");

        foreach (var item in order.OrderItems)
        {
            if (item.Product != null)
                item.Product.StockQuantity += item.Quantity;
        }

        order.Status = OrderStatus.Cancelled;
        await _context.SaveChangesAsync();

        await _notificationService.SendAsync(
            userId,
            "Sifariş ləğv edildi",
            $"#{order.Id} nömrəli sifarişiniz ləğv edildi.",
            "order",
            order.Id
        );
    }

    public async Task<OrderGetDto> CheckoutAsync(int userId, string lang, string? couponCode)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        var cartItems = await _context.CartItems
            .Include(x => x.Product)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        if (!cartItems.Any())
            throw new Exception("Cart is empty");

        foreach (var item in cartItems)
        {
            if (item.Product == null)
                throw new Exception("Product not found");

            if (item.Product.StockQuantity < item.Quantity)
                throw new Exception($"Not enough stock for product: {item.Product.NameAz}");
        }

        var totalPrice = cartItems.Sum(x => x.Product.Price * x.Quantity);

        var order = new Order
        {
            UserId = userId,
            Status = OrderStatus.Pending,
            TotalPrice = totalPrice,
            OrderItems = cartItems.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                ProductImageUrl = x.Product.ImageUrl,
                Quantity = x.Quantity,
                Price = x.Product.Price
            }).ToList()
        };

        await _context.Orders.AddAsync(order);

        foreach (var item in cartItems)
            item.Product.StockQuantity -= item.Quantity;

        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        await _notificationService.SendAsync(
            userId,
            "Sifariş verildi",
            $"#{order.Id} nömrəli sifarişiniz qəbul edildi. Ümumi məbləğ: {order.TotalPrice} AZN.",
            "order",
            order.Id
        );

        return Map(order, lang);
    }

    public async Task RejectOrderAsync(int orderId, string reason)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == orderId)
            ?? throw new KeyNotFoundException("Order not found");

        if (order.Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Delivered order cannot be rejected");

        if (order.Status == OrderStatus.Shipped)
            throw new InvalidOperationException("Shipped order cannot be rejected");

        order.Status = OrderStatus.Rejected;
        order.RejectReason = reason;
        await _context.SaveChangesAsync();

        await _notificationService.SendAsync(
            order.UserId,
            "Sifariş rədd edildi",
            $"#{order.Id} nömrəli sifarişiniz rədd edildi. Səbəb: {reason}",
            "order",
            order.Id
        );
    }
    // (optional future: coupon logic)
    // if (couponCode is not null) { apply discount }
    public async Task<List<OrderGetDto>> GetMyOrdersAsync(int userId, string lang)
    {
        var orders = await _context.Orders
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return orders.Select(x => Map(x, lang)).ToList();
    }

    public async Task UpdateStatusAsync(int orderId, OrderStatus status)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == orderId)
            ?? throw new KeyNotFoundException("Order not found");

        if (!CanMove(order.Status, status))
            throw new InvalidOperationException($"Invalid transition {order.Status} → {status}");

        order.Status = status;
        await _context.SaveChangesAsync();

        var message = status switch
        {
            OrderStatus.Accepted => "Sifarişiniz qəbul edildi.",
            OrderStatus.Shipped => "Sifarişiniz göndərildi.",
            OrderStatus.Delivered => "Sifarişiniz çatdırıldı.",
            _ => "Sifariş statusunuz yeniləndi."
        };

        await _notificationService.SendAsync(
            order.UserId,
            "Sifariş statusu dəyişdi",
            message,
            "order",
            order.Id
        );
    }

    private OrderGetDto Map(Order order, string lang)
    {
        return new OrderGetDto
        {
            Id = order.Id,
            TotalPrice = order.TotalPrice,
            Status = order.Status.ToString(),
            CreatedAt = order.CreatedAt,
            Items = order.OrderItems.Select(x => new OrderItemGetDto
            {
                ProductName = x.Product.NameAz,
                ProductImageUrl = x.Product.ImageUrl,
                Quantity = x.Quantity,
                Price = x.Price
            }).ToList()
        };
    }
}