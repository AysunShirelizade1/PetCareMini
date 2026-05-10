using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByRefreshTokenAsync(string refreshToken); 
    Task<bool> IsEmailExistAsync(string email);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}