using PetCareMini.Application.DTOs.Blog;

namespace PetCareMini.Application.Abstracts.Services;

public interface IBlogAuthorProfileService
{
    Task<BlogAuthorProfileDto?> GetByUserIdAsync(int userId);
    Task CreateOrUpdateAsync(int userId, BlogAuthorProfileDto dto);
}