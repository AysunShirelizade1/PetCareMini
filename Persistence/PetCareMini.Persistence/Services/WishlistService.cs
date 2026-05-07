using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.WishlistItem;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IProductRepository _productRepository;

    public WishlistService(
        IWishlistRepository wishlistRepository,
        IProductRepository productRepository)
    {
        _wishlistRepository = wishlistRepository;
        _productRepository = productRepository;
    }

    public async Task<List<WishlistItemGetDto>> GetUserWishlistAsync(
        int userId, string lang = "az")
    {
        var items = await _wishlistRepository.GetUserWishlistAsync(userId);

        return items.Select(x => new WishlistItemGetDto
        {
            Id = x.Id,
            ProductId = x.ProductId,
            ProductName = lang == "en" ? x.Product.NameEn : x.Product.NameAz,
            ProductPrice = x.Product.Price,
            ProductImageUrl = x.Product.ImageUrl
        }).ToList();
    }

    public async Task<bool> AddToWishlistAsync(int userId, int productId)
    {
        // Product exists check
        var productExists = await _productRepository.ExistsAsync(productId);
        if (!productExists)
            throw new KeyNotFoundException($"Product with id {productId} not found.");

        var existItem = await _wishlistRepository.GetByUserAndProductAsync(userId, productId);
        if (existItem is not null)
            return false; // artıq wishlist-dədir

        await _wishlistRepository.AddAsync(new WishlistItem
        {
            UserId = userId,
            ProductId = productId
        });
        await _wishlistRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveFromWishlistAsync(int userId, int productId)
    {
        var item = await _wishlistRepository.GetByUserAndProductAsync(userId, productId);
        if (item is null)
            return false; // tapılmadı

        _wishlistRepository.Delete(item);
        await _wishlistRepository.SaveChangesAsync();

        return true;
    }
}