using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class BlogTag : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;

    public ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();
}