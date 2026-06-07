using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Admin;
using PetCareMini.Application.DTOs.User;
using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Services;

public class AdminService : IAdminService
{
    private readonly AppDbContext _context;

    public AdminService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<UserGetDto>> GetAllUsersAsync()
    {
        return await _context.Users
            .Select(u => new UserGetDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role.ToString() 
            })
            .ToListAsync();
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
        var lowStockProducts = await _context.Products
            .Where(p => p.IsActive && p.StockQuantity < 5)
            .Select(p => new LowStockProductDto
            {
                Id = p.Id,
                Name = p.NameAz,
                StockQuantity = p.StockQuantity
            })
            .ToListAsync();
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
            LowStockProducts = lowStockProducts,
            TopProducts = topProducts
        };
    }
    public async Task ChangeUserRoleAsync(int userId, UserRole role)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)
            ?? throw new KeyNotFoundException("User not found");

        if (user.Role == UserRole.Admin && role != UserRole.Admin)
            throw new InvalidOperationException("Cannot demote an Admin");

        user.Role = role;

        if (role == UserRole.Veterinarian)
        {
            var alreadyExists = await _context.Veterinarians
                .AnyAsync(v => v.UserId == userId);

            if (!alreadyExists)
            {
                await _context.Veterinarians.AddAsync(new Veterinarian
                {
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    IsAvailable = true
                });
            }
        }
        else
        {
            var vet = await _context.Veterinarians
                .FirstOrDefaultAsync(v => v.UserId == userId);

            if (vet != null)
            {
                // Əvvəlcə Appointments-ları sil
                var appointments = _context.Appointments
                    .Where(a => a.VeterinarianId == vet.Id);
                _context.Appointments.RemoveRange(appointments);

                // VeterinaryReviews varsa onları da sil
                var vetReviews = _context.VeterinaryReviews
                    .Where(r => r.VeterinarianId == vet.Id);
                _context.VeterinaryReviews.RemoveRange(vetReviews);

                _context.Veterinarians.Remove(vet);
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)
            ?? throw new KeyNotFoundException("User not found");

        if (user.Role == UserRole.Admin)
            throw new InvalidOperationException("Admin user cannot be deleted");

        // Bütün bağlı məlumatları düzgün sıra ilə sil
            await _context.Database.ExecuteSqlRawAsync(@"
        DELETE FROM ""BlogComments"" WHERE ""ParentCommentId"" IN (SELECT ""Id"" FROM ""BlogComments"" WHERE ""UserId"" = {0});
        DELETE FROM ""BlogComments"" WHERE ""UserId"" = {0};
        DELETE FROM ""BlogPostTags"" WHERE ""BlogPostId"" IN (SELECT ""Id"" FROM ""BlogPosts"" WHERE ""AuthorId"" = {0});
        DELETE FROM ""BlogPosts"" WHERE ""AuthorId"" = {0};
        DELETE FROM ""BlogAuthorProfiles"" WHERE ""UserId"" = {0};
        DELETE FROM ""ProductReviews"" WHERE ""UserId"" = {0};
        DELETE FROM ""VeterinaryReviews"" WHERE ""UserId"" = {0};
        DELETE FROM ""WishlistItems"" WHERE ""UserId"" = {0};
        DELETE FROM ""CartItems"" WHERE ""UserId"" = {0};
        DELETE FROM ""Appointments"" WHERE ""UserId"" = {0};
        DELETE FROM ""OrderItems"" WHERE ""OrderId"" IN (SELECT ""Id"" FROM ""Orders"" WHERE ""UserId"" = {0});
        DELETE FROM ""Orders"" WHERE ""UserId"" = {0};
        DELETE FROM ""Pets"" WHERE ""UserId"" = {0};
        DELETE FROM ""Users"" WHERE ""Id"" = {0};
    ", userId);
    }
}