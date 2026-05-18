using PetCareMini.Application.DTOs.WishlistItem;

namespace PetCareMini.Application.Abstracts.Services;

public interface IWishlistService
{
    Task<List<WishlistItemGetDto>> GetUserWishlistAsync(int userId, string lang = "az");
    Task<bool> AddToWishlistAsync(int userId, int productId);

    Task<bool> RemoveFromWishlistAsync(int userId, int productId);
}