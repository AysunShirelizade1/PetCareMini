using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class ProductReview : BaseEntity
{
    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}