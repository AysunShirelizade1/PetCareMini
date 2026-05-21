using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Blog;
using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;

namespace PetCareMini.Persistence.Services;

public class BlogPostService : IBlogPostService
{
    private readonly IBlogPostRepository _repo;
    private readonly IBlogTagRepository _tagRepo;
    private readonly IBlogAuthorProfileRepository _profileRepo;

    public BlogPostService(
        IBlogPostRepository repo,
        IBlogTagRepository tagRepo,
        IBlogAuthorProfileRepository profileRepo)
    {
        _repo = repo;
        _tagRepo = tagRepo;
        _profileRepo = profileRepo;
    }

    public async Task<List<BlogPostSummaryDto>> GetAllPublishedAsync()
    {
        var posts = await _repo.GetAllPublishedAsync();
        return posts.Select(MapToSummary).ToList();
    }

    public async Task<List<BlogPostSummaryDto>> GetByCategoryAsync(string categorySlug)
    {
        var posts = await _repo.GetAllPublishedAsync();
        return posts
            .Where(x => x.Category.Slug == categorySlug)
            .Select(MapToSummary)
            .ToList();
    }

    public async Task<BlogPostGetDto> GetBySlugAsync(string slug)
    {
        var post = await _repo.GetBySlugAsync(slug)
            ?? throw new KeyNotFoundException($"Post '{slug}' tapılmadı.");

        // ViewCount artır
        post.ViewCount++;
        _repo.Update(post);
        await _repo.SaveChangesAsync();

        return MapToDto(post);
    }

    public async Task CreateAsync(int userId, BlogPostCreateDto dto)
    {
        var tags = await _tagRepo.GetByIdsAsync(dto.TagIds);

        var post = new BlogPost
        {
            Title = dto.Title,
            Slug = GenerateSlug(dto.Title),
            Content = dto.Content,
            Summary = dto.Summary,
            CoverImageUrl = dto.CoverImageUrl,
            CategoryId = dto.CategoryId,
            AuthorId = userId,
            Status = BlogPostStatus.Pending,
            ReadTimeMinutes = CalculateReadTime(dto.Content),
            BlogPostTags = tags.Select(t => new BlogPostTag { TagId = t.Id }).ToList()
        };

        await _repo.AddAsync(post);
        await _repo.SaveChangesAsync();

        // Author profile TotalPosts artır
        await UpdateAuthorTotalPostsAsync(userId);
    }

    public async Task UpdateAsync(int userId, int id, BlogPostUpdateDto dto)
    {
        var post = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Post tapılmadı.");

        if (post.AuthorId != userId)
            throw new UnauthorizedAccessException("Bu postu redaktə etmək icazəniz yoxdur.");

        var tags = await _tagRepo.GetByIdsAsync(dto.TagIds);

        post.Title = dto.Title;
        post.Slug = GenerateSlug(dto.Title);
        post.Content = dto.Content;
        post.Summary = dto.Summary;
        post.CoverImageUrl = dto.CoverImageUrl;
        post.CategoryId = dto.CategoryId;
        post.Status = BlogPostStatus.Pending;   // yenilənəndə yenidən təsdiq lazımdır
        post.ReadTimeMinutes = CalculateReadTime(dto.Content);
        post.BlogPostTags = tags.Select(t => new BlogPostTag
        {
            BlogPostId = post.Id,
            TagId = t.Id
        }).ToList();

        _repo.Update(post);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userId, int id)
    {
        var post = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Post tapılmadı.");

        if (post.AuthorId != userId)
            throw new UnauthorizedAccessException("Bu postu silmək icazəniz yoxdur.");

        _repo.Delete(post);
        await _repo.SaveChangesAsync();

        await UpdateAuthorTotalPostsAsync(userId);
    }

    public async Task<List<BlogPostSummaryDto>> GetAllAsync()
    {
        var posts = await _repo.GetAllAsync();
        return posts.Select(MapToSummary).ToList();
    }

    public async Task<List<BlogPostSummaryDto>> GetPendingAsync()
    {
        var posts = await _repo.GetPendingAsync();
        return posts.Select(MapToSummary).ToList();
    }

    public async Task ApproveAsync(int id)
    {
        var post = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Post tapılmadı.");

        post.Status = BlogPostStatus.Published;
        post.PublishedAt = DateTime.UtcNow;

        _repo.Update(post);
        await _repo.SaveChangesAsync();
    }

    public async Task RejectAsync(int id, BlogRejectDto dto)
    {
        var post = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Post tapılmadı.");

        post.Status = BlogPostStatus.Rejected;
        post.RejectionReason = dto.RejectionReason;

        _repo.Update(post);
        await _repo.SaveChangesAsync();
    }

    // ── Helpers ──────────────────────────────────────────────

    private async Task UpdateAuthorTotalPostsAsync(int userId)
    {
        var profile = await _profileRepo.GetByUserIdAsync(userId);
        if (profile is null) return;

        var posts = await _repo.GetByAuthorAsync(userId);
        profile.TotalPosts = posts.Count(x => x.Status == BlogPostStatus.Published);
        _profileRepo.Update(profile);
        await _profileRepo.SaveChangesAsync();
    }

    private static int CalculateReadTime(string content)
    {
        var wordCount = content.Split(' ',
            StringSplitOptions.RemoveEmptyEntries).Length;
        return Math.Max(1, wordCount / 200);    // orta oxuma sürəti: 200 söz/dəq
    }

    private static string GenerateSlug(string title)
        => title.ToLower()
            .Replace(" ", "-")
            .Replace("ə", "e").Replace("ı", "i")
            .Replace("ö", "o").Replace("ü", "u")
            .Replace("ç", "c").Replace("ş", "s")
            .Replace("ğ", "g");

    private static BlogPostSummaryDto MapToSummary(BlogPost p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Slug = p.Slug,
        Summary = p.Summary,
        CoverImageUrl = p.CoverImageUrl,
        ReadTimeMinutes = p.ReadTimeMinutes,
        ViewCount = p.ViewCount,
        PublishedAt = p.PublishedAt,
        AuthorName = p.Author.FullName,
        AuthorImageUrl = p.Author.ImageUrl,
        CategoryName = p.Category.Name,
        Tags = p.BlogPostTags.Select(t => t.Tag.Name).ToList()
    };

    private static BlogPostGetDto MapToDto(BlogPost p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Slug = p.Slug,
        Content = p.Content,
        Summary = p.Summary,
        CoverImageUrl = p.CoverImageUrl,
        ReadTimeMinutes = p.ReadTimeMinutes,
        ViewCount = p.ViewCount,
        Status = p.Status.ToString(),
        RejectionReason = p.RejectionReason,
        PublishedAt = p.PublishedAt,
        CreatedAt = p.CreatedAt,
        AuthorId = p.AuthorId,
        AuthorName = p.Author.FullName,
        AuthorImageUrl = p.Author.ImageUrl,
        AuthorBio = p.Author.BlogAuthorProfile?.Bio,
        CategoryName = p.Category.Name,
        CategorySlug = p.Category.Slug,
        Tags = p.BlogPostTags.Select(t => t.Tag.Name).ToList(),
        Comments = p.Comments
            .Where(c => c.IsApproved && c.ParentCommentId == null)
            .Select(c => new BlogCommentGetDto
            {
                Id = c.Id,
                Content = c.Content,
                Rating = c.Rating,
                UserName = c.User.FullName,
                UserImageUrl = c.User.ImageUrl,
                CreatedAt = c.CreatedAt,
                ParentCommentId = c.ParentCommentId,
                Replies = c.Replies
                    .Where(r => r.IsApproved)
                    .Select(r => new BlogCommentGetDto
                    {
                        Id = r.Id,
                        Content = r.Content,
                        Rating = r.Rating,
                        UserName = r.User.FullName,
                        UserImageUrl = r.User.ImageUrl,
                        CreatedAt = r.CreatedAt
                    }).ToList()
            }).ToList()
    };
}