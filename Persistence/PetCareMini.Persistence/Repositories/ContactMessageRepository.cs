using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Persistence.Repositories;

public class ContactMessageRepository : IContactMessageRepository
{
    private readonly AppDbContext _context;

    public ContactMessageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ContactMessage> CreateAsync(ContactMessage message)
    {
        await _context.ContactMessages.AddAsync(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<ContactMessage?> GetByIdAsync(int id)
        => await _context.ContactMessages
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<ContactMessage>> GetAllAsync()
        => await _context.ContactMessages
            .Include(x => x.User)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<List<ContactMessage>> GetByUserIdAsync(int userId)
        => await _context.ContactMessages
            .Include(x => x.User)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<List<ContactMessage>> GetUnreadAsync()
        => await _context.ContactMessages
            .Include(x => x.User)
            .Where(x => !x.IsRead)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<List<ContactMessage>> GetArchivedAsync()
        => await _context.ContactMessages
            .Include(x => x.User)
            .Where(x => x.IsArchived)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task UpdateAsync(ContactMessage message)
    {
        _context.ContactMessages.Update(message);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ContactMessage message)
    {
        _context.ContactMessages.Remove(message);
        await _context.SaveChangesAsync();
    }
}