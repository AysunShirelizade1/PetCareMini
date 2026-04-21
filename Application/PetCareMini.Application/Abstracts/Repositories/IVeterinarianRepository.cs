using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IVeterinarianRepository
{
    Task<List<Veterinarian>> GetAllAsync();
    Task<Veterinarian?> GetByIdAsync(int id);
    void Update(Veterinarian veterinarian);
    void Delete(Veterinarian veterinarian);
    Task AddAsync(Veterinarian veterinarian);
    Task SaveChangesAsync();
}
