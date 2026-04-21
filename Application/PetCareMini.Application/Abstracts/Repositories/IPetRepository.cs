using PetCareMini.Domain.Entities;
namespace PetCareMini.Application.Abstracts.Repositories;

public interface IPetRepository
{
    Task<List<Pet>> GetAllByOwnerIdAsync(int ownerId);
    Task<Pet?> GetByIdAsync(int id);
    void Update(Pet pet);
    void Delete(Pet pet);
    Task AddAsync(Pet pet);
    Task SaveChangesAsync();
}
