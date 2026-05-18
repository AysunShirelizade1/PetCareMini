
namespace PetCareMini.Application.DTOs.Admin;

public class AdminStatisticsDto
{
    public int TotalProducts { get; set; }
    public int TotalUsers { get; set; }
    public int TotalOrders { get; set;  }
    public decimal TotalRevenue { get; set; }
    public int TotalReviews { get; set; }
    public int ActiveCoupons { get; set; }
    public int LowStockCount { get; set; }// Number of products with low stock
    public List<TopProductDto> TopProducts { get; set; } = new();

}
public class  TopProductDto
{
    public string Name { get; set; } = string.Empty;
    public int OrderCount { get; set; }
}