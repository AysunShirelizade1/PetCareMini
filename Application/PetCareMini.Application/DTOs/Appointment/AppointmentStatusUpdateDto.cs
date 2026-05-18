using System.ComponentModel.DataAnnotations;

namespace PetCareMini.Application.DTOs.Appointment;

public class AppointmentStatusUpdateDto
{
    
    public int Status { get; set; } // 0=Pending, 1=Approved, 2=Completed, 3=Canceled
}