using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class Product : BaseEntity
{
    public string NameAz { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;

    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }

    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public int CategoryId { get; set; }
    public ProductCategory Category { get; set; } = null!;

    public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}