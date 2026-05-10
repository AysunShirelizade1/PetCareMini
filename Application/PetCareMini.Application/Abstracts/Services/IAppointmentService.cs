using PetCareMini.Application.DTOs.Appointment;

namespace PetCareMini.Application.Abstracts.Services;

public interface IAppointmentService
{
    Task CreateAsync(int userId, AppointmentCreateDto dto);
    Task<List<AppointmentGetDto>> GetUserAppointmentsAsync(int userId, string lang = "az");
    Task<List<AppointmentGetDto>> GetAllAsync(string lang = "az");
    Task UpdateStatusAsync(int appointmentId, AppointmentStatusUpdateDto dto);
}