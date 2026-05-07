namespace PetCareMini.Application.DTOs.Product;

public class ProductQueryDto
{
    public string Lang { get; set; } = "az";
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Search { get; set; }
    public string? SortBy { get; set; }


}
