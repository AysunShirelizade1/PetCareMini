using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IWishlistRepository
{
    Task<List<WishlistItem>> GetUserWishlistAsync(int userId);

    Task<WishlistItem?> GetByUserAndProductAsync(int userId, int productId);

    Task AddAsync(WishlistItem wishlistItem);

    void Delete(WishlistItem wishlistItem);

    Task SaveChangesAsync();
}