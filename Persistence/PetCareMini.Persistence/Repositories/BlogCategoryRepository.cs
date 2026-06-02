using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class BlogCategoryRepository : IBlogCategoryRepository
{
    private readonly AppDbContext _context;

    public BlogCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BlogCategory>> GetAllAsync()
        => await _context.BlogCategories
            .OrderBy(x => x.NameAz)
            .ToListAsync();

    public async Task<BlogCategory?> GetByIdAsync(int id)
        => await _context.BlogCategories
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<BlogCategory?> GetBySlugAsync(string slug, string lang = "az")
        => await _context.BlogCategories
            .FirstOrDefaultAsync(x => lang == "az" ? x.SlugAz == slug : x.SlugEn == slug);

    public async Task AddAsync(BlogCategory category)
        => await _context.BlogCategories.AddAsync(category);

    public void Update(BlogCategory category)
        => _context.BlogCategories.Update(category);

    public void Delete(BlogCategory category)
        => _context.BlogCategories.Remove(category);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}