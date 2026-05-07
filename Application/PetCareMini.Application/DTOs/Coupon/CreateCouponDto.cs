namespace PetCareMini.Application.DTOs.Coupon;

public class CreateCouponDto
{
    public string Code { get; set; } = string.Empty;
    public decimal DiscountPercent { get; set; }
    public DateTime ExpireDate { get; set; }
}