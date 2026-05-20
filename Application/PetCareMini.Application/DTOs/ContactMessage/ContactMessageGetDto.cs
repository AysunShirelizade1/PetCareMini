using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.ContactMessage;

public class ContactMessageGetDto
{
    public int Id { get; set; }
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; } = false;
    public bool IsArchived { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public string? ReplyMessage { get; set; }
    public DateTime? RepliedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    //User
    public string UserFullName { get; set; } = null!;
    public string UserEmail { get; set; } = null!;
}

