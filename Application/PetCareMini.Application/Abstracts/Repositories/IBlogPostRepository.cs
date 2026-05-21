using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IBlogPostRepository
{
    Task<List<BlogPost>> GetAllPublishedAsync();
    Task<List<BlogPost>> GetAllAsync();                          // Admin üçün
    Task<List<BlogPost>> GetByCategoryAsync(int categoryId);
    Task<List<BlogPost>> GetByAuthorAsync(int authorId);
    Task<List<BlogPost>> GetPendingAsync();                      // Admin — gözləyənlər
    Task<BlogPost?> GetByIdAsync(int id);
    Task<BlogPost?> GetBySlugAsync(string slug);
    Task AddAsync(BlogPost post);
    void Update(BlogPost post);
    void Delete(BlogPost post);
    Task SaveChangesAsync();
}