using PetCareMini.Domain.Entities;
namespace PetCareMini.Application.Abstracts.Repositories;

public interface IServiceRepository
{
    Task<List<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(int id);
    Task AddAsync(Service service);
    void Update(Service service);
    void Delete(Service service);
    Task SaveChangesAsync();
}
