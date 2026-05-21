namespace PetCareMini.Application.DTOs.Blog;

public class BlogAuthorProfileDto
{
    public string Bio { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? LinkedInUrl { get; set; }
}