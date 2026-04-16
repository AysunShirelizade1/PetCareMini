using PetCareMini.Domain.Entities;
namespace PetCareMini.Application.Interfaces.Repositories;

public interface IAppointmentRepository
{
    Task<List<Appointment>> GetAllByOwnerIdAsync(int ownerId);
    Task<Appointment?> GetByIdAsync(int id);
    void Update(Appointment appointment);
    void Delete(Appointment appointment);
    Task AddAsync(Appointment appointment);
    Task SaveChangesAsync();
}
