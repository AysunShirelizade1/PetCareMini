namespace PetCareMini.Application.DTOs.Appointment;

public class AppointmentStatusUpdateDto
{
    public int Status { get; set; } // 1=Pending, 2=Approved, 3=Completed, 4=Canceled
}