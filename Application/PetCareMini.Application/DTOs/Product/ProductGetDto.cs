namespace PetCareMini.Application.DTOs.Product;

public class ProductGetDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public string CategoryName { get; set; } = string.Empty;
}