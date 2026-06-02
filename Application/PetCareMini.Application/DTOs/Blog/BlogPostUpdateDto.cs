namespace PetCareMini.Application.DTOs.Blog;

public class BlogPostUpdateDto
{
    public string TitleAz { get; set; } = null!;
    public string TitleEn { get; set; } = null!;
    public string ContentAz { get; set; } = null!;
    public string ContentEn { get; set; } = null!;
    public string SummaryAz { get; set; } = null!;
    public string SummaryEn { get; set; } = null!;
    public string? CoverImageUrl { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; } = new();
}