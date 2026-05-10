namespace PetCareMini.Application.DTOs.Appointment;

public class AppointmentCreateDto
{
    public int PetId { get; set; }
    public int VeterinarianId { get; set; }
    public int ServiceId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? Notes { get; set; }
}