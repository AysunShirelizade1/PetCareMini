using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class FaqRepository : IFaqRepository
{
    private readonly AppDbContext _context;

    public FaqRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Faq>> GetAllAsync()
    {
        return await _context.Faqs
            .Where(x => x.IsActive)
            .ToListAsync();
    }

    public async Task<Faq?> GetByIdAsync(int id)
    {
        return await _context.Faqs.FindAsync(id);
    }

    public async Task AddAsync(Faq faq)
    {
        await _context.Faqs.AddAsync(faq);
    }

    public void Update(Faq faq)
    {
        _context.Faqs.Update(faq);
    }

    public void Delete(Faq faq)
    {
        _context.Faqs.Remove(faq);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}