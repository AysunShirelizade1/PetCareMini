using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.WishlistItem;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;

    public WishlistService(IWishlistRepository wishlistRepository)
    {
        _wishlistRepository = wishlistRepository;
    }

    public async Task<List<WishlistItemGetDto>> GetUserWishlistAsync(int userId)
    {
        var items = await _wishlistRepository.GetUserWishlistAsync(userId);

        return items.Select(x => new WishlistItemGetDto
        {
            Id = x.Id,
            ProductId = x.ProductId,
            ProductName = x.Product.Name,
            ProductPrice = x.Product.Price,
            ProductImageUrl = x.Product.ImageUrl
        }).ToList();
    }

    public async Task<bool> AddToWishlistAsync(int userId, int productId)
    {
        var existItem = await _wishlistRepository.GetByUserAndProductAsync(userId, productId);

        if (existItem is not null)
            return false;

        var wishlistItem = new WishlistItem
        {
            UserId = userId,
            ProductId = productId
        };

        await _wishlistRepository.AddAsync(wishlistItem);
        await _wishlistRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveFromWishlistAsync(int userId, int productId)
    {
        var item = await _wishlistRepository.GetByUserAndProductAsync(userId, productId);

        if (item is null)
            return false;

        _wishlistRepository.Delete(item);
        await _wishlistRepository.SaveChangesAsync();

        return true;
    }
}