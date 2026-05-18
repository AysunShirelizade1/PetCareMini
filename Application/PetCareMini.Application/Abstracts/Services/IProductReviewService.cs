using PetCareMini.Application.DTOs.Review;

namespace PetCareMini.Application.Abstracts.Services;

public interface IProductReviewService
{
    Task<bool> CreateAsync(int userId, ReviewCreateDto dto);

    Task<List<ReviewGetDto>> GetProductReviewsAsync(int productId);
}