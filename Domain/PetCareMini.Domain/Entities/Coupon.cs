namespace PetCareMini.Domain.Entities;

public class Coupon
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal DiscountPercent { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime ExpireDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}