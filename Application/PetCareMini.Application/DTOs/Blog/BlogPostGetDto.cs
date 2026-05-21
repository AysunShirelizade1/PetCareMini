namespace PetCareMini.Application.DTOs.Blog;

public class BlogPostGetDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Summary { get; set; } = null!;
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
    public string CategoryName { get; set; } = null!;
    public string CategorySlug { get; set; } = null!;

    // Etiketlər
    public List<string> Tags { get; set; } = new();

    // Şərhlər
    public List<BlogCommentGetDto> Comments { get; set; } = new();
}
