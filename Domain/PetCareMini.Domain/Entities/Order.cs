using PetCareMini.Domain.Common;
using PetCareMini.Domain.Entities;

public class Order : BaseEntity
{
    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = "Completed";

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();
}