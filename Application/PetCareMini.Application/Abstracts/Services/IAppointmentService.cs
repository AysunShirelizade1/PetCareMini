using PetCareMini.Application.DTOs.Appointment;

namespace PetCareMini.Application.Abstracts.Services;

public interface IAppointmentService
{
    Task CreateAsync(int userId, AppointmentCreateDto dto);
    Task<List<AppointmentGetDto>> GetUserAppointmentsAsync(int userId);
    Task<List<AppointmentGetDto>> GetAllAsync();
    Task UpdateStatusAsync(int appointmentId, AppointmentStatusUpdateDto dto);
}