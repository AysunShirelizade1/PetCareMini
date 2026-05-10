using System.ComponentModel.DataAnnotations;

namespace PetCareMini.Application.DTOs.Appointment;

public class AppointmentStatusUpdateDto
{
    // Added Range validation so invalid numbers are rejected
    [Range(0, 3, ErrorMessage = "Status must be between 0 and 3.")]
    public int Status { get; set; } // 0=Pending, 1=Approved, 2=Completed, 3=Canceled
}