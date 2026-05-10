using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IPetRepository
{
    Task<List<Pet>> GetUserPetsAsync(int ownerId);
    Task<Pet?> GetByIdAsync(int id);
    Task CreateAsync(Pet pet);
    void Delete(Pet pet);
    Task SaveChangesAsync();
}