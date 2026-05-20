using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.ContactMessage;

public class ContactMessageCreateDto
{
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
}
