using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class VeterinarianRepository : IVeterinarianRepository
{
    private readonly AppDbContext _context;

    public VeterinarianRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Veterinarian>> GetAllAsync()
    {
        return await _context.Veterinarians.ToListAsync();
    }

    public async Task<Veterinarian?> GetByIdAsync(int id)
    {
        return await _context.Veterinarians.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Veterinarian veterinarian)
    {
        await _context.Veterinarians.AddAsync(veterinarian);
    }

    public void Update(Veterinarian veterinarian)
    {
        _context.Veterinarians.Update(veterinarian);
    }

    public void Delete(Veterinarian veterinarian)
    {
        _context.Veterinarians.Remove(veterinarian);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}