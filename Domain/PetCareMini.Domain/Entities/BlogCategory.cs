using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class BlogCategory : BaseEntity
{
    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }
    public string? IconUrl { get; set; }

    public ICollection<BlogPost> Posts { get; set; } = new List<BlogPost>();
}