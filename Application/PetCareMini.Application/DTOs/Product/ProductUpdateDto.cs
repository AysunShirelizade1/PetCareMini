
namespace PetCareMini.Application.DTOs.Product;

public class ProductUpdateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }
}
