using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class Notification : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; 
    public bool IsRead { get; set; } = false;
    public int? ReferenceId { get; set; } 
}