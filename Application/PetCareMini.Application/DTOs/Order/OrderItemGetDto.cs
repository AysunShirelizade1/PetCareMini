namespace PetCareMini.Application.DTOs.Order;

public class OrderItemGetDto
{
    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Total => Price * Quantity;
}