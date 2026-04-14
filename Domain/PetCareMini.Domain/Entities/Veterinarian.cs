using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class Veterinarian : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Specialty { get; set; }
    public int? ExperienceYears { get; set; }
    public string? Bio { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? FacebookUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public bool IsAvailable { get; set; } = true;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}