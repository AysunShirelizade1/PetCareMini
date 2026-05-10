namespace PetCareMini.Application.Abstracts.Repositories;

using PetCareMini.Domain.Entities;

public interface IAppointmentRepository
{
    Task CreateAsync(Appointment appointment);
    Task<Appointment?> GetByIdAsync(int id);
    Task<List<Appointment>> GetUserAppointmentsAsync(int userId);
    Task<List<Appointment>> GetAllAsync();
    Task<bool> ExistsConflictAsync(int veterinarianId, DateTime appointmentDate);
    Task SaveChangesAsync();
}