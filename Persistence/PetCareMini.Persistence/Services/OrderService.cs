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

    public OrderService(AppDbContext context)
    {
        _context = context;
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
    public async Task<List<OrderAdminDto>> GetAllAsync(
        string lang,
        string? status,
        int page,
        int pageSize)
    {
        var query = _context.Orders
            .Include(x => x.User)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status) &&
            Enum.TryParse<OrderStatus>(status, true, out var parsed))
        {
            query = query.Where(x => x.Status == parsed);
        }

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
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        }).ToList();
    }

    public async Task<OrderGetDto> CheckoutAsync(
    int userId,
    string lang,
    string? couponCode)
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
                throw new Exception(
                    $"Not enough stock for product: {item.Product.NameAz}");
        }

        var totalPrice = cartItems.Sum(x => x.Product.Price * x.Quantity);

        // (optional future: coupon logic)
        // if (couponCode is not null) { apply discount }

        var order = new Order
        {
            UserId = userId,
            Status = OrderStatus.Pending,
            TotalPrice = totalPrice,
            OrderItems = cartItems.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Product.Price
            }).ToList()
        };

        await _context.Orders.AddAsync(order);

        
        foreach (var item in cartItems)
        {
            item.Product.StockQuantity -= item.Quantity;
        }

        _context.CartItems.RemoveRange(cartItems);

 
        await _context.SaveChangesAsync();

        await transaction.CommitAsync();

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
    }
    public async Task<List<OrderGetDto>> GetMyOrdersAsync(
        int userId,
        string lang)
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
            throw new InvalidOperationException(
                $"Invalid transition {order.Status} → {status}");

        order.Status = status;

        await _context.SaveChangesAsync();
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
                Quantity = x.Quantity,
                Price = x.Price
            }).ToList()
        };
    }
}