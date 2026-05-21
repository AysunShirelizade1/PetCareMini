using PetCareMini.Application.DTOs.Blog;

namespace PetCareMini.Application.Abstracts.Services;

public interface IBlogPostService
{
    // Hamıya açıq
    Task<List<BlogPostSummaryDto>> GetAllPublishedAsync();
    Task<List<BlogPostSummaryDto>> GetByCategoryAsync(string categorySlug);
    Task<BlogPostGetDto> GetBySlugAsync(string slug);

    // Login user
    Task CreateAsync(int userId, BlogPostCreateDto dto);
    Task UpdateAsync(int userId, int id, BlogPostUpdateDto dto);
    Task DeleteAsync(int userId, int id);

    // Admin
    Task<List<BlogPostSummaryDto>> GetAllAsync();
    Task<List<BlogPostSummaryDto>> GetPendingAsync();
    Task ApproveAsync(int id);
    Task RejectAsync(int id, BlogRejectDto dto);
}