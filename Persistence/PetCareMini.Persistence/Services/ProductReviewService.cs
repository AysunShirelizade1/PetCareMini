using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Review;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Services;

public class ProductReviewService : IProductReviewService
{
    private readonly AppDbContext _context;

    public ProductReviewService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(int userId, ReviewCreateDto dto)
    {
        var alreadyReviewed = await _context.ProductReviews
            .AnyAsync(x => x.UserId == userId && x.ProductId == dto.ProductId);

        if (alreadyReviewed)
            return false;

        var review = new ProductReview
        {
            UserId = userId,
            ProductId = dto.ProductId,
            Rating = dto.Rating,
            Comment = dto.Comment
        };

        await _context.ProductReviews.AddAsync(review);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<ReviewGetDto>> GetProductReviewsAsync(int productId)
    {
        return await _context.ProductReviews
            .Include(x => x.User)
            .Where(x => x.ProductId == productId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new ReviewGetDto
            {
                UserName = x.User.FullName,
                Rating = x.Rating,
                Comment = x.Comment,
                CreatedAt = (DateTime)x.CreatedAt
            })
            .ToListAsync();
    }
}