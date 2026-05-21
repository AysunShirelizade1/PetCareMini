using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IBlogCommentRepository
{
    Task<List<BlogComment>> GetByPostIdAsync(int postId);
    Task<BlogComment?> GetByIdAsync(int id);
    Task AddAsync(BlogComment comment);
    void Delete(BlogComment comment);
    Task SaveChangesAsync();
}