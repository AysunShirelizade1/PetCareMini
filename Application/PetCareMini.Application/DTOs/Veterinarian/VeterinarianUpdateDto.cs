using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Veterinarian;

public class VeterinarianUpdateDto
{
    public string FullName { get; set; } = null!;
    public string? Specialty { get; set; }
    public string? Bio { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? FacebookUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? LinkedInUrl { get; set; }
}