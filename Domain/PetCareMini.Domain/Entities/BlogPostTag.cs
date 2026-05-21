namespace PetCareMini.Domain.Entities;

public class BlogPostTag
{
    public int BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; } = null!;

    public int TagId { get; set; }
    public BlogTag Tag { get; set; } = null!;
}