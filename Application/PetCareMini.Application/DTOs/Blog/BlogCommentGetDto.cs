namespace PetCareMini.Application.DTOs.Blog;

public class BlogCommentGetDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int Rating { get; set; }
    public string UserName { get; set; } = null!;
    public string? UserImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? ParentCommentId { get; set; }
    public List<BlogCommentGetDto> Replies { get; set; } = new();
}