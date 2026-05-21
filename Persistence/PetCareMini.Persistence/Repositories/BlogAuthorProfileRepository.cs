using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class BlogAuthorProfileRepository : IBlogAuthorProfileRepository
{
    private readonly AppDbContext _context;

    public BlogAuthorProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BlogAuthorProfile?> GetByUserIdAsync(int userId)
        => await _context.BlogAuthorProfiles
            .FirstOrDefaultAsync(x => x.UserId == userId);

    public async Task AddAsync(BlogAuthorProfile profile)
        => await _context.BlogAuthorProfiles.AddAsync(profile);

    public void Update(BlogAuthorProfile profile)
        => _context.BlogAuthorProfiles.Update(profile);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}