using PetCareMini.Application.DTOs.Blog;

namespace PetCareMini.Application.Abstracts.Services;

public interface IBlogCategoryService
{
    Task<List<BlogCategoryGetDto>> GetAllAsync(string lang = "az");
    Task<BlogCategoryGetDto> GetBySlugAsync(string slug, string lang = "az");
    Task CreateAsync(BlogCategoryCreateDto dto);
    Task DeleteAsync(int id);
}
