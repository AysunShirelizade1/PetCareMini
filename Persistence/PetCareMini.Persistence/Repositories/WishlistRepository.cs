using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class WishlistRepository : IWishlistRepository
{
    private readonly AppDbContext _context;

    public WishlistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<WishlistItem>> GetUserWishlistAsync(int userId)
    {
        return await _context.WishlistItems
            .Include(x => x.Product)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<WishlistItem?> GetByUserAndProductAsync(int userId, int productId)
    {
        return await _context.WishlistItems
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
    }

    public async Task AddAsync(WishlistItem wishlistItem)
    {
        await _context.WishlistItems.AddAsync(wishlistItem);
    }

    public void Delete(WishlistItem wishlistItem)
    {
        _context.WishlistItems.Remove(wishlistItem);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}