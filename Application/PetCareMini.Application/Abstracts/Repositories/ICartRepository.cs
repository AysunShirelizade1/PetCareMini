
using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface ICartRepository
{
    Task<List<CartItem>> GetUserCartAsync(int userId);

    Task<CartItem?> GetAsync(int userId, int productId);

    Task AddAsync(CartItem item);

    void Update(CartItem item);

    void Delete(CartItem item);

    Task SaveChangesAsync();
}
