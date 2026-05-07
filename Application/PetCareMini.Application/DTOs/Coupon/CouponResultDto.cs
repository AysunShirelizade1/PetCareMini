namespace PetCareMini.Application.DTOs.Coupon;

public class CouponResultDto
{
    public string Code { get; set; } = string.Empty;
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalPrice { get; set; }
}