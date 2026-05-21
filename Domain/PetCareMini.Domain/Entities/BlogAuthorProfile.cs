using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class BlogAuthorProfile : BaseEntity
{
    public string Bio { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public int TotalPosts { get; set; } = 0;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}