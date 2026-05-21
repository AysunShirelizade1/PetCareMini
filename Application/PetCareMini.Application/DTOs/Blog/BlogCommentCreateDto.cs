namespace PetCareMini.Application.DTOs.Blog;

public class BlogCommentCreateDto
{
    public int BlogPostId { get; set; }
    public string Content { get; set; } = null!;
    public int Rating { get; set; }
    public int? ParentCommentId { get; set; }
}
