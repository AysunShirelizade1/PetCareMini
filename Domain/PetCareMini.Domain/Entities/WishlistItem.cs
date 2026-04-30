using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class WishlistItem : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}