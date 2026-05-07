using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.CartItem;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepo;
    private readonly IProductRepository _productRepo;

    public CartService(ICartRepository cartRepo, IProductRepository productRepo)
    {
        _cartRepo = cartRepo;
        _productRepo = productRepo;
    }

    public async Task<List<CartItemGetDto>> GetCartAsync(int userId, string lang = "az")
    {
        var items = await _cartRepo.GetUserCartAsync(userId);

        return items.Select(x => new CartItemGetDto
        {
            ProductId = x.ProductId,
            ProductName = lang == "en" ? x.Product.NameEn : x.Product.NameAz,
            Price = x.Product.Price,
            Quantity = x.Quantity
        }).ToList();
    }

    public async Task AddToCartAsync(int userId, int productId)
    {
        // Product exists check
        var exists = await _productRepo.ExistsAsync(productId);
        if (!exists)
            throw new KeyNotFoundException($"Product with id {productId} not found.");

        var item = await _cartRepo.GetAsync(userId, productId);

        if (item != null)
        {
            item.Quantity++;
            _cartRepo.Update(item);
        }
        else
        {
            await _cartRepo.AddAsync(new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            });
        }

        await _cartRepo.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(int userId, int productId)
    {
        var item = await _cartRepo.GetAsync(userId, productId);
        if (item == null) return;

        _cartRepo.Delete(item);
        await _cartRepo.SaveChangesAsync();
    }

    public async Task ChangeQuantityAsync(int userId, int productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0.");

        var item = await _cartRepo.GetAsync(userId, productId);
        if (item == null)
            throw new KeyNotFoundException("Cart item not found.");

        item.Quantity = quantity;
        _cartRepo.Update(item);
        await _cartRepo.SaveChangesAsync();
    }
}