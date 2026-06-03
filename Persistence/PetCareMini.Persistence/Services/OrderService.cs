using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Order;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<OrderAdminDto>> GetAllAsync(string lang, string? status = null, int page = 1, int pageSize = 20)
    {
        var query = _context.Orders
            .Include(x => x.User)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(x => x.Status == status);

        var orders = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var isEn = lang.ToLower() == "en";

        return orders.Select(x => new OrderAdminDto
        {
            Id = x.Id,
            UserFullName = x.User.FullName,
            UserEmail = x.User.Email,
            TotalPrice = x.TotalPrice,
            Status = x.Status,
            CreatedAt = x.CreatedAt,
            Items = x.OrderItems.Select(i => new OrderItemGetDto
            {
                ProductName = isEn ? i.Product.NameEn : i.Product.NameAz,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        }).ToList();
    }
    public async Task<OrderGetDto?> CheckoutAsync(int userId, string lang, string? couponCode = null)
    {
        var cartItems = await _context.CartItems
            .Include(x => x.Product)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        if (!cartItems.Any())
            throw new InvalidOperationException("Your cart is empty.");

        var totalPrice = cartItems.Sum(x => x.Product.Price * x.Quantity);
        var discountAmount = 0m;
        var usedCoupon = string.Empty;

        if (!string.IsNullOrWhiteSpace(couponCode))
        {
            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(c =>
                    c.Code == couponCode.ToUpper().Trim() &&
                    c.IsActive &&
                    c.ExpireDate > DateTime.UtcNow);

            if (coupon is null)
                throw new KeyNotFoundException("Coupon not found or expired.");

            discountAmount = Math.Round(totalPrice * coupon.DiscountPercent / 100, 2);
            usedCoupon = coupon.Code;
        }

        var finalPrice = totalPrice - discountAmount;

        var order = new Order
        {
            UserId = userId,
            TotalPrice = finalPrice,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            OrderItems = cartItems.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Product.Price
            }).ToList()
        };

        await _context.Orders.AddAsync(order);

        _context.CartItems.RemoveRange(cartItems);

        await _context.SaveChangesAsync();

        var createdOrder = await _context.Orders
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == order.Id);

        return createdOrder is null ? null : MapToDto(createdOrder, lang);
    }

    public async Task<List<OrderGetDto>> GetMyOrdersAsync(int userId, string lang)
    {
        var orders = await _context.Orders
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return orders.Select(x => MapToDto(x, lang)).ToList();
    }

    private OrderGetDto MapToDto(Order order, string lang)
    {
        var isEn = lang.ToLower() == "en";

        return new OrderGetDto
        {
            Id = order.Id,
            TotalPrice = order.TotalPrice,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            Items = order.OrderItems.Select(x => new OrderItemGetDto
            {
                ProductName = isEn ? x.Product.NameEn : x.Product.NameAz,
                Quantity = x.Quantity,
                Price = x.Price
            }).ToList()
        };
    }
}