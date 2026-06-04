using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class BlogComment : BaseEntity
{
    public string Content { get; set; } = null!;
    public int Rating { get; set; }
    public bool IsApproved { get; set; } = true;

    public int? ParentCommentId { get; set; }
    public BlogComment? ParentComment { get; set; }
    public ICollection<BlogComment> Replies { get; set; } = new List<BlogComment>();

    public int BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}