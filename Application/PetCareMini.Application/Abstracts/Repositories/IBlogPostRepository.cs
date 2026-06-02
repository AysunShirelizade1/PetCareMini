using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IBlogPostRepository
{
    Task<List<BlogPost>> GetAllPublishedAsync();
    Task<int> CountPendingByAuthorAsync(int userId);
    Task<List<BlogPost>> GetAllAsync();                         
    Task<List<BlogPost>> GetByCategoryAsync(int categoryId);
    Task<List<BlogPost>> GetByAuthorAsync(int authorId);
    Task<List<BlogPost>> GetPendingAsync();                      
    Task<BlogPost?> GetByIdAsync(int id);
    Task<BlogPost?> GetBySlugAsync(string slug, string lang = "az");
    Task AddAsync(BlogPost post);
    void Update(BlogPost post);
    void Delete(BlogPost post);
    Task SaveChangesAsync();
}