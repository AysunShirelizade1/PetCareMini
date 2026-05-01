using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.CartItem;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repo;

    public CartService(ICartRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<CartItemGetDto>> GetCartAsync(int userId)
    {
        var items = await _repo.GetUserCartAsync(userId);

        return items.Select(x => new CartItemGetDto
        {
            ProductId = x.ProductId,
            ProductName = x.Product.NameAz,
            Price = x.Product.Price,
            Quantity = x.Quantity
        }).ToList();
    }

    public async Task AddToCartAsync(int userId, int productId)
    {
        var item = await _repo.GetAsync(userId, productId);

        if (item != null)
        {
            item.Quantity++;
            _repo.Update(item);
        }
        else
        {
            await _repo.AddAsync(new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            });
        }

        await _repo.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(int userId, int productId)
    {
        var item = await _repo.GetAsync(userId, productId);

        if (item != null)
        {
            _repo.Delete(item);
            await _repo.SaveChangesAsync();
        }
    }

    public async Task ChangeQuantityAsync(int userId, int productId, int quantity)
    {
        var item = await _repo.GetAsync(userId, productId);

        if (item == null) return;

        item.Quantity = quantity;

        _repo.Update(item);
        await _repo.SaveChangesAsync();
    }
}