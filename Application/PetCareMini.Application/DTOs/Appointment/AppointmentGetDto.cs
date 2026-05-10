namespace PetCareMini.Application.DTOs.Appointment;

public class AppointmentGetDto
{
    public int Id { get; set; }
    public string PetName { get; set; } = string.Empty;
    public string VeterinarianName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}