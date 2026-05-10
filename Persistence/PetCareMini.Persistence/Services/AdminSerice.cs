using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Admin;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Services;

public class AdminService : IAdminService
{
    private readonly AppDbContext _context;

    public AdminService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AdminStatisticsDto> GetStatisticsAsync()
    {
        var totalProducts = await _context.Products
            .CountAsync(p => p.IsActive);

        var totalUsers = await _context.Users
            .CountAsync();

        var totalOrders = await _context.Orders
            .CountAsync();

        var totalRevenue = await _context.Orders
            .SumAsync(o => o.TotalPrice);

        var totalReviews = await _context.ProductReviews
            .CountAsync();

        var activeCoupons = await _context.Coupons
            .CountAsync(c => c.IsActive && c.ExpireDate > DateTime.UtcNow);

        var lowStockCount = await _context.Products
            .CountAsync(p => p.IsActive && p.StockQuantity < 5);

        var topProducts = await _context.OrderItems
            .GroupBy(oi => oi.Product.NameAz)
            .Select(g => new TopProductDto
            {
                Name = g.Key,
                OrderCount = g.Sum(x => x.Quantity)
            })
            .OrderByDescending(x => x.OrderCount)
            .Take(5)
            .ToListAsync();

        return new AdminStatisticsDto
        {
            TotalProducts = totalProducts,
            TotalUsers = totalUsers,
            TotalOrders = totalOrders,
            TotalRevenue = totalRevenue,
            TotalReviews = totalReviews,
            ActiveCoupons = activeCoupons,
            LowStockCount = lowStockCount,
            TopProducts = topProducts
        };
    }
}