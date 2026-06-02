using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class BlogTag : BaseEntity
{
    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;

    public ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();
}