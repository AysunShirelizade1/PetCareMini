namespace PetCareMini.Application.DTOs.Admin;

public class LowStockProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int StockQuantity { get; set; }
}