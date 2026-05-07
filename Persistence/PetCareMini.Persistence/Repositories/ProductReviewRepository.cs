using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class ProductReviewRepository : IProductReviewRepository
{
    private readonly AppDbContext _context;

    public ProductReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasUserReviewedAsync(int userId, int productId)
        => await _context.ProductReviews
            .AnyAsync(x => x.UserId == userId && x.ProductId == productId);

    public async Task AddAsync(ProductReview review)
        => await _context.ProductReviews.AddAsync(review);

    public async Task<List<ProductReview>> GetByProductIdAsync(int productId)
        => await _context.ProductReviews
            .Include(x => x.User)
            .Where(x => x.ProductId == productId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}