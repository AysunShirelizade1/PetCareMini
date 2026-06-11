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
    private readonly INotificationService _notificationService;

    private readonly ICloudinaryService _cloudinaryService;

    public BlogPostService(
        IBlogPostRepository repo,
        IBlogTagRepository tagRepo,
        IBlogAuthorProfileRepository profileRepo,
        INotificationService notificationService,
        ICloudinaryService cloudinaryService)
    {
        _repo = repo;
        _tagRepo = tagRepo;
        _profileRepo = profileRepo;
        _notificationService = notificationService;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<List<BlogPostSummaryDto>> GetAllPublishedAsync(string lang = "az")
    {
        var posts = await _repo.GetAllPublishedAsync();
        return posts.Select(p => MapToSummary(p, lang)).ToList();
    }

    public async Task<List<BlogPostSummaryDto>> GetByCategoryAsync(string categorySlug, string lang = "az")
    {
        var posts = await _repo.GetAllPublishedAsync();
        return posts
            .Where(x => (lang == "az" ? x.Category.SlugAz : x.Category.SlugEn) == categorySlug)
            .Select(p => MapToSummary(p, lang))
            .ToList();
    }
    public async Task<List<BlogPostSummaryDto>> GetMyPostsAsync(int userId, string lang = "az")
    {
        var posts = await _repo.GetByAuthorAsync(userId);
        return posts.Select(p => MapToSummary(p, lang)).ToList();
    }
    public async Task<BlogPostGetDto> GetBySlugAsync(string slug, string lang = "az")
    {
        var post = await _repo.GetBySlugAsync(slug, lang)
            ?? throw new KeyNotFoundException($"Post '{slug}' tapılmadı.");

      
        post.ViewCount++;
        _repo.Update(post);
        await _repo.SaveChangesAsync();

        return MapToDto(post);
    }

    public async Task CreateAsync(int userId, BlogPostCreateDto dto)
    {

        var pendingCount = await _repo.CountPendingByAuthorAsync(userId);
        if (pendingCount >= 2)
            throw new InvalidOperationException("Eyni anda maksimum 2 gözləyən postunuz ola bilər.");

    
        if (dto.TitleAz.Length < 10 || dto.TitleAz.Length > 150)
            throw new ArgumentException("Başlıq 10-150 simvol arasında olmalıdır.");
        if (dto.TitleEn.Length < 10 || dto.TitleEn.Length > 150)
            throw new ArgumentException("Title must be between 10 and 150 characters.");

        if (dto.SummaryAz.Length < 10 || dto.SummaryAz.Length > 300)
            throw new ArgumentException("Xülasə 10-300 simvol arasında olmalıdır.");
        if (dto.SummaryEn.Length < 10 || dto.SummaryEn.Length > 300)
            throw new ArgumentException("Summary must be between 10 and 300 characters.");

        if (dto.ContentAz.Length < 15 || dto.ContentAz.Length > 10000)
            throw new ArgumentException("Məzmun 15-10,000 simvol arasında olmalıdır.");
        if (dto.ContentEn.Length < 15 || dto.ContentEn.Length > 10000)
            throw new ArgumentException("Content must be between 15 and 10,000 characters.");
        var tags = await _tagRepo.GetByIdsAsync(dto.TagIds);
        string? coverImageUrl = null;
        if (dto.CoverImage != null)
        {
            var (url, _) = await _cloudinaryService.UploadImageAsync(dto.CoverImage, "petcaremini/blogs");
            coverImageUrl = url;
        }
        var post = new BlogPost
        {
            TitleAz = dto.TitleAz,
            TitleEn = dto.TitleEn,
            SlugAz = GenerateSlug(dto.TitleAz),
            SlugEn = GenerateSlug(dto.TitleEn),
            ContentAz = dto.ContentAz,
            ContentEn = dto.ContentEn,
            SummaryAz = dto.SummaryAz,
            SummaryEn = dto.SummaryEn,
            CoverImageUrl = coverImageUrl,
            CategoryId = dto.CategoryId,
            AuthorId = userId,
            Status = BlogPostStatus.Pending,
            ReadTimeMinutes = CalculateReadTime(dto.ContentAz),
            BlogPostTags = tags.Select(t => new BlogPostTag { TagId = t.Id }).ToList()
        };

        await _repo.AddAsync(post);
        await _repo.SaveChangesAsync();

        
        await UpdateAuthorTotalPostsAsync(userId);
    }

    public async Task UpdateAsync(int userId, int id, BlogPostUpdateDto dto)
    {
        
        var pendingCount = await _repo.CountPendingByAuthorAsync(userId);
        if (pendingCount >= 2)
            throw new InvalidOperationException("Eyni anda maksimum 2 gözləyən postunuz ola bilər.");

        
        if (dto.TitleAz.Length < 10 || dto.TitleAz.Length > 150)
            throw new ArgumentException("Başlıq 10-150 simvol arasında olmalıdır.");
        if (dto.TitleEn.Length < 10 || dto.TitleEn.Length > 150)
            throw new ArgumentException("Title must be between 10 and 150 characters.");

        if (dto.SummaryAz.Length < 50 || dto.SummaryAz.Length > 300)
            throw new ArgumentException("Xülasə 50-300 simvol arasında olmalıdır.");
        if (dto.SummaryEn.Length < 50 || dto.SummaryEn.Length > 300)
            throw new ArgumentException("Summary must be between 50 and 300 characters.");

        if (dto.ContentAz.Length < 300 || dto.ContentAz.Length > 10000)
            throw new ArgumentException("Məzmun 300-10,000 simvol arasında olmalıdır.");
        if (dto.ContentEn.Length < 300 || dto.ContentEn.Length > 10000)
            throw new ArgumentException("Content must be between 300 and 10,000 characters.");
        var post = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Post tapılmadı.");

        if (post.AuthorId != userId)
            throw new UnauthorizedAccessException("Bu postu redaktə etmək icazəniz yoxdur.");

        var tags = await _tagRepo.GetByIdsAsync(dto.TagIds);

        post.TitleAz = dto.TitleAz;
        post.TitleEn = dto.TitleEn;
        post.SlugAz = GenerateSlug(dto.TitleAz);
        post.SlugEn = GenerateSlug(dto.TitleEn);
        post.ContentAz = dto.ContentAz;
        post.ContentEn = dto.ContentEn;
        post.SummaryAz = dto.SummaryAz;
        post.SummaryEn = dto.SummaryEn;
        if (dto.CoverImage != null)
        {
            var (url, _) = await _cloudinaryService.UploadImageAsync(dto.CoverImage, "petcaremini/blogs");
            post.CoverImageUrl = url;
        }
        post.CategoryId = dto.CategoryId;
        post.Status = BlogPostStatus.Pending;   
        post.ReadTimeMinutes = CalculateReadTime(dto.ContentAz);
        post.BlogPostTags = tags.Select(t => new BlogPostTag
        {
            BlogPostId = post.Id,
            TagId = t.Id
        }).ToList();

        _repo.Update(post);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userId, int id, bool isAdmin = false)
    {
        var post = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Post tapılmadı.");

        if (!isAdmin && post.AuthorId != userId)
            throw new UnauthorizedAccessException("Bu postu silmək icazəniz yoxdur.");

        _repo.Delete(post);
        await _repo.SaveChangesAsync();

        await UpdateAuthorTotalPostsAsync(post.AuthorId);
    }

    public async Task<List<BlogPostSummaryDto>> GetAllAsync(string lang = "az")
    {
        var posts = await _repo.GetAllAsync();
        return posts.Select(p => MapToSummary(p, lang)).ToList();
    }

    public async Task<List<BlogPostSummaryDto>> GetPendingAsync(string lang = "az")
    {
        var posts = await _repo.GetPendingAsync();
        return posts.Select(p => MapToSummary(p, lang)).ToList();
    }

    public async Task ApproveAsync(int id)
    {
        var post = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Post tapılmadı.");

        post.Status = BlogPostStatus.Published;
        post.PublishedAt = DateTime.UtcNow;

        _repo.Update(post);
        await _repo.SaveChangesAsync();

        await _notificationService.SendAsync(
            post.AuthorId,
            "Blog postunuz təsdiqləndi",
            $"\"{post.TitleAz}\" adlı postunuz yayımlandı.",
            "blog",
            post.Id
        );
    }

    public async Task RejectAsync(int id, BlogRejectDto dto)
    {
        var post = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Post tapılmadı.");

        post.Status = BlogPostStatus.Rejected;
        post.RejectionReason = dto.RejectionReason;

        _repo.Update(post);
        await _repo.SaveChangesAsync();

        await _notificationService.SendAsync(
            post.AuthorId,
            "Blog postunuz rədd edildi",
            $"\"{post.TitleAz}\" adlı postunuz rədd edildi. Səbəb: {dto.RejectionReason}",
            "blog",
            post.Id
        );
    }


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

    private static BlogPostSummaryDto MapToSummary(BlogPost p, string lang) => new()
    {
        Id = p.Id,

        Title = lang == "az"
        ? p.TitleAz
        : p.TitleEn,

        Slug = lang == "az"
        ? p.SlugAz
        : p.SlugEn,

        Summary = lang == "az"
        ? p.SummaryAz
        : p.SummaryEn,

        CoverImageUrl = p.CoverImageUrl,
        Status = p.Status.ToString(),
        ReadTimeMinutes = p.ReadTimeMinutes,
        ViewCount = p.ViewCount,

        PublishedAt = p.PublishedAt,

        AuthorName = p.Author.FullName,
        AuthorImageUrl = p.Author.ImageUrl,

        CategoryName = lang == "az"
        ? p.Category.NameAz
        : p.Category.NameEn,

        Tags = p.BlogPostTags
        .Select(t => lang == "az"
            ? t.Tag.NameAz
            : t.Tag.NameEn)
        .ToList()
    };

    private static BlogPostGetDto MapToDto(BlogPost p) => new()
    {
        Id = p.Id,
        TitleAz = p.TitleAz,
        TitleEn = p.TitleEn,
        SlugAz = p.SlugAz,
        SlugEn = p.SlugEn,
        ContentAz = p.ContentAz,
        ContentEn = p.ContentEn,
        SummaryAz = p.SummaryAz,
        SummaryEn = p.SummaryEn,
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
        CategoryNameAz = p.Category.NameAz,
        CategoryNameEn = p.Category.NameEn,
        CategorySlugAz = p.Category.SlugAz,
        CategorySlugEn = p.Category.SlugEn,
        Tags = p.BlogPostTags.Select(t => t.Tag.NameAz).ToList(),
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