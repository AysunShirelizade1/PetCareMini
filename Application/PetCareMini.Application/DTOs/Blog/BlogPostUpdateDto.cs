namespace PetCareMini.Application.DTOs.Blog;

public class BlogPostUpdateDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string? CoverImageUrl { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; } = new();
}