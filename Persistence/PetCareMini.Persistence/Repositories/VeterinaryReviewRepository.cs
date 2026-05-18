using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Persistence.Repositories;

public class VeterinaryReviewRepository : IVeterinaryReviewRepository
{
    private readonly AppDbContext _context;
    public VeterinaryReviewRepository(AppDbContext context)
        => _context = context;

    public async Task<IEnumerable<VeterinaryReview>> GetByVeterinarianAsync(int vetId, string lang = "az")
        => await _context.VeterinaryReviews
            .Include(r => r.User)
            .Include(r => r.Service)
            .Where(r => r.VeterinarianId == vetId && r.IsApproved)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<VeterinaryReview>> GetFeaturedAsync()
        => await _context.VeterinaryReviews
            .Include(r => r.User)
            .Include(r => r.Veterinarian)
            .Where(r => r.IsFeatured && r.IsApproved)
            .ToListAsync();

    public async Task<IEnumerable<VeterinaryReview>> GetAllApprovedAsync()
        => await _context.VeterinaryReviews
            .Include(r => r.User)
            .Include(r => r.Veterinarian)
            .Include(r => r.Service)
            .Where(r => r.IsApproved)
            .OrderByDescending(r => r.Rating)
            .ToListAsync();

    public async Task AddAsync(VeterinaryReview review)
        => await _context.VeterinaryReviews.AddAsync(review);

    public async Task<VeterinaryReview?> GetByIdAsync(int id)
        => await _context.VeterinaryReviews.FindAsync(id);
    public async Task DeleteAsync(VeterinaryReview review)
    => _context.VeterinaryReviews.Remove(review);

    public async Task SaveAsync()
        => await _context.SaveChangesAsync();
}
