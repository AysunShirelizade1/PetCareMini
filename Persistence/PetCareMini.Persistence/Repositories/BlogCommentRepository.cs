using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class BlogCommentRepository : IBlogCommentRepository
{
    private readonly AppDbContext _context;

    public BlogCommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BlogComment>> GetByPostIdAsync(int postId)
        => await _context.BlogComments
            .Include(x => x.User)
            .Include(x => x.Replies)
                .ThenInclude(x => x.User)
            .Where(x => x.BlogPostId == postId
                     && x.IsApproved
                     && x.ParentCommentId == null)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<BlogComment?> GetByIdAsync(int id)
        => await _context.BlogComments
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(BlogComment comment)
        => await _context.BlogComments.AddAsync(comment);

    public void Delete(BlogComment comment)
        => _context.BlogComments.Remove(comment);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}