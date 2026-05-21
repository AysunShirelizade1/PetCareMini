using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class BlogTagRepository : IBlogTagRepository
{
    private readonly AppDbContext _context;

    public BlogTagRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BlogTag>> GetAllAsync()
        => await _context.BlogTags
            .OrderBy(x => x.Name)
            .ToListAsync();

    public async Task<BlogTag?> GetByIdAsync(int id)
        => await _context.BlogTags
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<BlogTag>> GetByIdsAsync(List<int> ids)
        => await _context.BlogTags
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();

    public async Task AddAsync(BlogTag tag)
        => await _context.BlogTags.AddAsync(tag);

    public void Delete(BlogTag tag)
        => _context.BlogTags.Remove(tag);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}