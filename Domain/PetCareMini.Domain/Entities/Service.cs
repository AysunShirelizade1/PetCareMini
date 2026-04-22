using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int? DurationMinutes { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}