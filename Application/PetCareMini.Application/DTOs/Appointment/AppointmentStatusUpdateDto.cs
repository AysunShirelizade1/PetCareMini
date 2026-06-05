using System.ComponentModel.DataAnnotations;

namespace PetCareMini.Application.DTOs.Appointment;

public class AppointmentStatusUpdateDto
{
    
    public int Status { get; set; }
    public string? RejectionReason { get; set; }
}