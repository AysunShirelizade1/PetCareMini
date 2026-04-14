using PetCareMini.Domain.Entities;
namespace PetCareMini.Application.Interfaces.Repositories;

public interface IServiceRepository
{
    Task<List<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(int id);
    void Update(Service service);
    void Delete(Service service);
    Task AddAsync(Service service);
    Task SaveChangesAsync();
}
