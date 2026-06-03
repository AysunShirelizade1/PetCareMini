using PetCareMini.Application.DTOs.Appointment;

namespace PetCareMini.Application.Abstracts.Services;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentGetDto>> GetByUserAsync(int userId, string lang = "az");
    Task CreateAsync(int userId, AppointmentCreateDto dto);
    Task<List<AppointmentGetDto>> GetVetAppointmentsAsync(int vetUserId, string lang = "az");
    Task<List<AppointmentGetDto>> GetUserAppointmentsAsync(int userId, string lang = "az");
    Task<List<AppointmentGetDto>> GetAllAsync(string lang = "az");
    Task UpdateStatusAsync(int appointmentId, AppointmentStatusUpdateDto dto, int? vetUserId = null);
}