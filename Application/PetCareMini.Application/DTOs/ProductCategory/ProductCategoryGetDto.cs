namespace PetCareMini.Application.DTOs.ProductCategory;

public class ProductCategoryGetDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}