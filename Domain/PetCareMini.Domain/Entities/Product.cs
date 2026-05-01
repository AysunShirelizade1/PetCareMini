using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public int CategoryId { get; set; }
    public ProductCategory Category { get; set; } = null!;
    public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}