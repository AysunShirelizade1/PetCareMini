using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IBlogAuthorProfileRepository
{
    Task<BlogAuthorProfile?> GetByUserIdAsync(int userId);
    Task AddAsync(BlogAuthorProfile profile);
    void Update(BlogAuthorProfile profile);
    Task SaveChangesAsync();
}
