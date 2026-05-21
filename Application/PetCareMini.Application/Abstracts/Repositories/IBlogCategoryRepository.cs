using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IBlogCategoryRepository
{
    Task<List<BlogCategory>> GetAllAsync();
    Task<BlogCategory?> GetByIdAsync(int id);
    Task<BlogCategory?> GetBySlugAsync(string slug);
    Task AddAsync(BlogCategory category);
    void Update(BlogCategory category);
    void Delete(BlogCategory category);
    Task SaveChangesAsync();
}