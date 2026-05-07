namespace PetCareMini.Application.DTOs.Product;

public class ProductQueryDto
{
    public string Lang { get; set; } = "az";
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Search { get; set; }
    public string? SortBy { get; set; }

    // Pagination
    private int _pageSize = 10;
    public int Page { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 50 ? 50 : value;
    }
}