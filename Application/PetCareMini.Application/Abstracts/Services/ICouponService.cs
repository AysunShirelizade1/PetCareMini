using PetCareMini.Application.DTOs.Coupon;

namespace PetCareMini.Application.Abstracts.Services;

public interface ICouponService
{
    Task<CouponResultDto> ApplyAsync(int userId, string code);
    Task CreateAsync(string code, decimal discountPercent, DateTime expireDate);
    Task DeactivateAsync(int id);
}