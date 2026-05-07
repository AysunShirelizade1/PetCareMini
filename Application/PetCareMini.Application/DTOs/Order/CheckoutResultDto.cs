namespace PetCareMini.Application.DTOs.Order;

public class CheckoutResultDto
{
    public int OrderId { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalPrice { get; set; }
    public string CouponUsed { get; set; } = string.Empty;
}