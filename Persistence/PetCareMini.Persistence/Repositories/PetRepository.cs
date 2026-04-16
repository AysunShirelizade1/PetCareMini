using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Interfaces.Repositories;
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

    public async Task<List<Pet>> GetAllByOwnerIdAsync(int ownerId)
    {
        return await _context.Pets
            .Where(x => x.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task<Pet?> GetByIdAsync(int id)
    {
        return await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Pet pet)
    {
        await _context.Pets.AddAsync(pet);
    }

    public void Update(Pet pet)
    {
        _context.Pets.Update(pet);
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