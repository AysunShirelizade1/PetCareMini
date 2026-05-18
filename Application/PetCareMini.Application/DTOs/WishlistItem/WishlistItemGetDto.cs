namespace PetCareMini.Application.DTOs.WishlistItem;

public class WishlistItemGetDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal ProductPrice { get; set; }

    public string? ProductImageUrl { get; set; }
}