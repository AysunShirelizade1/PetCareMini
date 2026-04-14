using PetCareMini.Domain.Common;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Domain.Entities;

public class Pet : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Breed { get; set; }
    public decimal? Weight { get; set; }
    public string? Notes { get; set; }

    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}