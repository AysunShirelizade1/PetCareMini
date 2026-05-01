using PetCareMini.Application.DTOs.CartItem;

namespace PetCareMini.Application.Abstracts.Services;

public interface ICartService
{
    Task<List<CartItemGetDto>> GetCartAsync(int userId);

    Task AddToCartAsync(int userId, int productId);

    Task RemoveFromCartAsync(int userId, int productId);

    Task ChangeQuantityAsync(int userId, int productId, int quantity);
}