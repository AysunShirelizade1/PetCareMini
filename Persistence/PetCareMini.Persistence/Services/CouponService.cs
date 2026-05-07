using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Coupon;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Services;

public class CouponService : ICouponService
{
    private readonly AppDbContext _context;
    private readonly ICartService _cartService;

    public CouponService(AppDbContext context, ICartService cartService)
    {
        _context = context;
        _cartService = cartService;
    }

    public async Task<CouponResultDto> ApplyAsync(int userId, string code)
    {
        // Kupon mövcudluğu
        var coupon = await _context.Coupons
            .FirstOrDefaultAsync(c =>
                c.Code == code.ToUpper().Trim() &&
                c.IsActive &&
                c.ExpireDate > DateTime.UtcNow);

        if (coupon is null)
            throw new KeyNotFoundException("Coupon not found or expired.");

        // Cart-ı gətir, ümumi məbləği hesabla
        var cartItems = await _cartService.GetCartAsync(userId);

        if (!cartItems.Any())
            throw new InvalidOperationException("Your cart is empty.");

        var originalTotal = cartItems.Sum(x => x.Price * x.Quantity);
        var discountAmount = Math.Round(originalTotal * coupon.DiscountPercent / 100, 2);
        var finalPrice = Math.Round(originalTotal - discountAmount, 2);

        return new CouponResultDto
        {
            Code = coupon.Code,
            DiscountPercent = coupon.DiscountPercent,
            DiscountAmount = discountAmount,
            FinalPrice = finalPrice
        };
    }

    public async Task CreateAsync(string code, decimal discountPercent, DateTime expireDate)
    {
        if (discountPercent <= 0 || discountPercent > 100)
            throw new ArgumentException("Discount percent must be between 1 and 100.");

        var exists = await _context.Coupons
            .AnyAsync(c => c.Code == code.ToUpper().Trim());

        if (exists)
            throw new InvalidOperationException("Coupon code already exists.");

        await _context.Coupons.AddAsync(new Coupon
        {
            Code = code.ToUpper().Trim(),
            DiscountPercent = discountPercent,
            ExpireDate = expireDate,
            IsActive = true
        });

        await _context.SaveChangesAsync();
    }

    public async Task DeactivateAsync(int id)
    {
        var coupon = await _context.Coupons.FindAsync(id);
        if (coupon is null)
            throw new KeyNotFoundException($"Coupon with id {id} not found.");

        coupon.IsActive = false;
        await _context.SaveChangesAsync();
    }
}