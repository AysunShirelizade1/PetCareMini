using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IVeterinarianRepository
{
    Task<List<Veterinarian>> GetAllAsync();
    Task<Veterinarian?> GetByIdAsync(int id);
    Task AddAsync(Veterinarian veterinarian);
    void Update(Veterinarian veterinarian);
    void Delete(Veterinarian veterinarian);
    Task SaveChangesAsync();
}
