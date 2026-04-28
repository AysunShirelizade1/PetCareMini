using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByIdAsync(int id);

    Task AddAsync(User user);

    Task<bool> IsEmailExistAsync(string email);

    Task SaveChangesAsync();
}