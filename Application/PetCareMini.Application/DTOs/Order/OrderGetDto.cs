namespace PetCareMini.Application.DTOs.Order;

public class OrderGetDto
{
    public int Id { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public List<OrderItemGetDto> Items { get; set; } = new();
}