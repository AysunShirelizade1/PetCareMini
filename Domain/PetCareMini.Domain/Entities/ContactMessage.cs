using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class ContactMessage : BaseEntity
{
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; } = false;
    public bool IsArchived { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    //Adminickaa
    public string? ReplyMessage { get; set; }
    public DateTime? RepliedAt { get; set; }
    //Just loginickaa
    public int UserId { get; set; }
    public User User { get; set; } = null!;

}

