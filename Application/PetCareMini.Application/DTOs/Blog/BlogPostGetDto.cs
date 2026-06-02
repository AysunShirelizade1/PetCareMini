namespace PetCareMini.Application.DTOs.Blog;

public class BlogPostGetDto
{
    public int Id { get; set; }
    public string TitleAz { get; set; } = null!;
    public string TitleEn { get; set; } = null!;
    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string ContentAz { get; set; } = null!;
    public string ContentEn { get; set; } = null!;
    public string SummaryAz { get; set; } = null!;
    public string SummaryEn { get; set; } = null!;
    public string? CoverImageUrl { get; set; }
    public int ReadTimeMinutes { get; set; }
    public int ViewCount { get; set; }
    public string Status { get; set; } = null!;
    public string? RejectionReason { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    // Yazar
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;
    public string? AuthorImageUrl { get; set; }
    public string? AuthorBio { get; set; }

    // Kateqoriya
    public string CategoryNameAz { get; set; } = null!;
    public string CategoryNameEn { get; set; } = null!;
    public string CategorySlugAz { get; set; } = null!;
    public string CategorySlugEn { get; set; } = null!;

    // Etiketlər
    public List<string> Tags { get; set; } = new();

    // Şərhlər
    public List<BlogCommentGetDto> Comments { get; set; } = new();
}
