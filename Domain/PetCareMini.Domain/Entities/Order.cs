using PetCareMini.Domain.Common;
using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;

public class Order : BaseEntity
{
    public decimal TotalPrice { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public string? RejectReason { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}