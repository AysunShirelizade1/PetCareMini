using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly AppDbContext _context;

    public BlogPostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BlogPost>> GetAllPublishedAsync()
        => await _context.BlogPosts
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Include(x => x.BlogPostTags)
                .ThenInclude(x => x.Tag)
            .Where(x => x.Status == BlogPostStatus.Published)
            .OrderByDescending(x => x.PublishedAt)
            .ToListAsync();

    public async Task<List<BlogPost>> GetAllAsync()
        => await _context.BlogPosts
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Include(x => x.BlogPostTags)
                .ThenInclude(x => x.Tag)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<List<BlogPost>> GetByCategoryAsync(int categoryId)
        => await _context.BlogPosts
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Include(x => x.BlogPostTags)
                .ThenInclude(x => x.Tag)
            .Where(x => x.CategoryId == categoryId
                     && x.Status == BlogPostStatus.Published)
            .OrderByDescending(x => x.PublishedAt)
            .ToListAsync();

    public async Task<List<BlogPost>> GetByAuthorAsync(int authorId)
        => await _context.BlogPosts
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Include(x => x.BlogPostTags)
                .ThenInclude(x => x.Tag)
            .Where(x => x.AuthorId == authorId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<List<BlogPost>> GetPendingAsync()
        => await _context.BlogPosts
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Where(x => x.Status == BlogPostStatus.Pending)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

    public async Task<BlogPost?> GetByIdAsync(int id)
        => await _context.BlogPosts
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Include(x => x.BlogPostTags)
                .ThenInclude(x => x.Tag)
            .Include(x => x.Comments.Where(c => c.IsApproved && c.ParentCommentId == null))
                .ThenInclude(x => x.Replies.Where(r => r.IsApproved))
                    .ThenInclude(x => x.User)
            .Include(x => x.Comments)
                .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<BlogPost?> GetBySlugAsync(string slug, string lang = "az")
        => await _context.BlogPosts
            .Include(x => x.Author)
                .ThenInclude(x => x.BlogAuthorProfile)
            .Include(x => x.Category)
            .Include(x => x.BlogPostTags)
                .ThenInclude(x => x.Tag)
            .Include(x => x.Comments.Where(c => c.IsApproved && c.ParentCommentId == null))
                .ThenInclude(x => x.Replies.Where(r => r.IsApproved))
                    .ThenInclude(x => x.User)
            .Include(x => x.Comments)
                .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => (lang == "en" ? x.SlugEn : x.SlugAz) == slug
                                && x.Status == BlogPostStatus.Published);

    public async Task AddAsync(BlogPost post)
        => await _context.BlogPosts.AddAsync(post);

    public void Update(BlogPost post)
        => _context.BlogPosts.Update(post);

    public void Delete(BlogPost post)
        => _context.BlogPosts.Remove(post);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}