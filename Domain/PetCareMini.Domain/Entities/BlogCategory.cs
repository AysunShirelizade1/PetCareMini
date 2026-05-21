using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class BlogCategory : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }

    public ICollection<BlogPost> Posts { get; set; } = new List<BlogPost>();
}