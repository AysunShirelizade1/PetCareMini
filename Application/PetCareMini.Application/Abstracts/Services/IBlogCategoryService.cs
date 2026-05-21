using PetCareMini.Application.DTOs.Blog;

namespace PetCareMini.Application.Abstracts.Services;

public interface IBlogCategoryService
{
    Task<List<BlogCategoryGetDto>> GetAllAsync();
    Task<BlogCategoryGetDto> GetBySlugAsync(string slug);
    Task CreateAsync(BlogCategoryCreateDto dto);
    Task DeleteAsync(int id);
}
