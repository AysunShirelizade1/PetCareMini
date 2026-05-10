using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class PetRepository : IPetRepository
{
    private readonly AppDbContext _context;

    public PetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pet>> GetUserPetsAsync(int ownerId)
    {
        return await _context.Pets
            .Where(p => p.OwnerId == ownerId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Pet?> GetByIdAsync(int id)
    {
        return await _context.Pets
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task CreateAsync(Pet pet)
    {
        await _context.Pets.AddAsync(pet);
    }

    public void Delete(Pet pet)
    {
        _context.Pets.Remove(pet);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}