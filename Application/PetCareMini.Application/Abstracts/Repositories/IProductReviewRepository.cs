using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IProductReviewRepository
{
    Task<bool> HasUserReviewedAsync(int userId, int productId);
    Task AddAsync(ProductReview review);
    Task<List<ProductReview>> GetByProductIdAsync(int productId);
    Task SaveChangesAsync();
}