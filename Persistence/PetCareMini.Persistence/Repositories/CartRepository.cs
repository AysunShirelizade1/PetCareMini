using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CartItem>> GetUserCartAsync(int userId)
    {
        return await _context.CartItems
            .Include(x => x.Product)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<CartItem?> GetAsync(int userId, int productId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
    }

    public async Task AddAsync(CartItem item)
    {
        await _context.CartItems.AddAsync(item);
    }

    public void Update(CartItem item)
    {
        _context.CartItems.Update(item);
    }

    public void Delete(CartItem item)
    {
        _context.CartItems.Remove(item);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}