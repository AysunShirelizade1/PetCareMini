using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Contact;

public class ContactInfoDto
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? WorkingHours { get; set; }
    public string? FacebookUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? TiktokUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? WhatsappUrl { get; set; }
    public string? YoutubeUrl { get; set; }
}
