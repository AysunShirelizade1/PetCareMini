using PetCareMini.Domain.Common;
using PetCareMini.Domain.Enums;

namespace PetCareMini.Domain.Entities;

public class BlogPost : BaseEntity
{
    public string TitleAz { get; set; } = null!;
    public string TitleEn { get; set; } = null!;
    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string ContentAz { get; set; } = null!;
    public string ContentEn { get; set; } = null!;
    public string SummaryAz { get; set; } = null!;
    public string SummaryEn { get; set; } = null!;
    public string? CoverImageUrl { get; set; }
    public int ViewCount { get; set; } = 0;
    public int ReadTimeMinutes { get; set; } = 0;

    public BlogPostStatus Status { get; set; } = BlogPostStatus.Pending;
    public string? RejectionReason { get; set; }
    public DateTime? PublishedAt { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public int CategoryId { get; set; }
    public BlogCategory Category { get; set; } = null!;

    public ICollection<BlogComment> Comments { get; set; } = new List<BlogComment>();
    public ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();
}