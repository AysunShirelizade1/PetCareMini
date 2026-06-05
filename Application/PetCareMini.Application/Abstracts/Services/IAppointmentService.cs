using PetCareMini.Application.DTOs.Appointment;

namespace PetCareMini.Application.Abstracts.Services;

public interface IAppointmentService
{
    Task<AppointmentGetDto> CreateAsync(int userId, AppointmentCreateDto dto);
    Task<List<AppointmentGetDto>> GetVetAppointmentsAsync(int vetUserId, string lang = "az");
    Task<List<AppointmentGetDto>> GetUserAppointmentsAsync(int userId, string lang = "az");
    Task<List<AppointmentGetDto>> GetAllAsync(string lang = "az");
    Task CancelAsync(int appointmentId, int userId);
    Task UpdateStatusAsync(int appointmentId, AppointmentStatusUpdateDto dto, int? vetUserId = null);
}