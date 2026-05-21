namespace PetCareMini.Application.DTOs.Blog;

public class BlogPostSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string? CoverImageUrl { get; set; }
    public int ReadTimeMinutes { get; set; }
    public int ViewCount { get; set; }
    public DateTime? PublishedAt { get; set; }

    public string AuthorName { get; set; } = null!;
    public string? AuthorImageUrl { get; set; }
    public string CategoryName { get; set; } = null!;
    public List<string> Tags { get; set; } = new();
}