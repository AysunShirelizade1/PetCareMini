using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Review;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class ProductReviewService : IProductReviewService
{
    private readonly IProductReviewRepository _reviewRepo;
    private readonly IProductRepository _productRepo;

    public ProductReviewService(
        IProductReviewRepository reviewRepo,
        IProductRepository productRepo)
    {
        _reviewRepo = reviewRepo;
        _productRepo = productRepo;
    }

    public async Task<bool> CreateAsync(int userId, ReviewCreateDto dto)
    {
        // Product exists check
        var productExists = await _productRepo.ExistsAsync(dto.ProductId);
        if (!productExists)
            throw new KeyNotFoundException($"Product with id {dto.ProductId} not found.");

        // Rating validation
        if (dto.Rating < 1 || dto.Rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        // Duplicate check
        var alreadyReviewed = await _reviewRepo.HasUserReviewedAsync(userId, dto.ProductId);
        if (alreadyReviewed)
            return false;

        await _reviewRepo.AddAsync(new ProductReview
        {
            UserId = userId,
            ProductId = dto.ProductId,
            Rating = dto.Rating,
            Comment = dto.Comment
        });

        await _reviewRepo.SaveChangesAsync();
        return true;
    }

    public async Task<List<ReviewGetDto>> GetProductReviewsAsync(int productId)
    {
        // Product exists check
        var productExists = await _productRepo.ExistsAsync(productId);
        if (!productExists)
            throw new KeyNotFoundException($"Product with id {productId} not found.");

        var reviews = await _reviewRepo.GetByProductIdAsync(productId);

        return reviews.Select(x => new ReviewGetDto
        {
            UserName = x.User.FullName,
            Rating = x.Rating,
            Comment = x.Comment,
            CreatedAt = (DateTime)x.CreatedAt
        }).ToList();
    }
}