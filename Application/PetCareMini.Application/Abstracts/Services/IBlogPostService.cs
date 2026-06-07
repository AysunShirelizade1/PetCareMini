using PetCareMini.Application.DTOs.Blog;

namespace PetCareMini.Application.Abstracts.Services;

public interface IBlogPostService
{
    Task<List<BlogPostSummaryDto>> GetMyPostsAsync(int userId, string lang = "az");
    // Hamıya açıq
    Task<List<BlogPostSummaryDto>> GetAllPublishedAsync(string lang = "az");
    Task<List<BlogPostSummaryDto>> GetByCategoryAsync(string categorySlug, string lang = "az");
    Task<BlogPostGetDto> GetBySlugAsync(string slug, string lang = "az");

    // Login user
    Task CreateAsync(int userId, BlogPostCreateDto dto);
    Task UpdateAsync(int userId, int id, BlogPostUpdateDto dto);
    Task DeleteAsync(int userId, int id);

    // Admin
    Task<List<BlogPostSummaryDto>> GetAllAsync(string lang = "az");
    Task<List<BlogPostSummaryDto>> GetPendingAsync(string lang = "az");
    Task ApproveAsync(int id);
    Task RejectAsync(int id, BlogRejectDto dto);
}