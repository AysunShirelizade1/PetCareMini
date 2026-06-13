using Microsoft.AspNetCore.Http;

namespace PetCareMini.Application.DTOs.Product;

public class ProductCreateDto
{
    public string NameAz { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int StockQuantity { get; set; }
    public IFormFile? Image { get; set; }  
    public int CategoryId { get; set; }
}