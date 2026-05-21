using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IBlogTagRepository
{
    Task<List<BlogTag>> GetAllAsync();
    Task<BlogTag?> GetByIdAsync(int id);
    Task<List<BlogTag>> GetByIdsAsync(List<int> ids);
    Task AddAsync(BlogTag tag);
    void Delete(BlogTag tag);
    Task SaveChangesAsync();
}