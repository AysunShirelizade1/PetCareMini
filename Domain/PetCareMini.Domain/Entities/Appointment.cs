
using PetCareMini.Domain.Common;
using PetCareMini.Domain.Enums;

namespace PetCareMini.Domain.Entities;

public class Appointment : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int PetId { get; set; }
    public Pet Pet { get; set; } = null!;

    public int VeterinarianId { get; set; }
    public Veterinarian Veterinarian { get; set; } = null!;

    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;

    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string? Notes { get; set; }
}