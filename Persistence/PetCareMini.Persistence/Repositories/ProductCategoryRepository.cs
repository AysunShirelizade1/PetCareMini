using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly AppDbContext _context;

    public ProductCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductCategory>> GetAllAsync()
    {
        return await _context.ProductCategories.ToListAsync();
    }

    public async Task<ProductCategory?> GetByIdAsync(int id)
    {
        return await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(ProductCategory entity)
    {
        await _context.ProductCategories.AddAsync(entity);
    }

    public void Update(ProductCategory entity)
    {
        _context.ProductCategories.Update(entity);
    }

    public void Delete(ProductCategory entity)
    {
        _context.ProductCategories.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}