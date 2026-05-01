namespace PetCareMini.Application.DTOs.ProductCategory;

public class ProductCategoryCreateDto
{
    public string NameAz { get; set; } = string.Empty;

    public string NameEn { get; set; } = string.Empty;

    public string? DescriptionAz { get; set; }

    public string? DescriptionEn { get; set; }
}