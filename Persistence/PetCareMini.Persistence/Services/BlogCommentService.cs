using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Blog;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class BlogCommentService : IBlogCommentService
{
    private readonly IBlogCommentRepository _repo;

    public BlogCommentService(IBlogCommentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<BlogCommentGetDto>> GetByPostIdAsync(int postId)
    {
        var comments = await _repo.GetByPostIdAsync(postId);
        return comments.Select(MapToDto).ToList();
    }

    public async Task CreateAsync(int userId, BlogCommentCreateDto dto)
    {

        if (dto.Rating < 1 || dto.Rating > 5)
            throw new ArgumentException("Rating 1 ilə 5 arasında olmalıdır.");

        var comment = new BlogComment
        {
            BlogPostId = dto.BlogPostId,
            UserId = userId,
            Content = dto.Content,
            Rating = dto.Rating,
            ParentCommentId = dto.ParentCommentId,
            IsApproved = true
        };

        await _repo.AddAsync(comment);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userId, int id)
    {
        var comment = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Şərh tapılmadı.");

        if (comment.UserId != userId)
            throw new UnauthorizedAccessException("Bu şərhi silmək icazəniz yoxdur.");

        _repo.Delete(comment);
        await _repo.SaveChangesAsync();
    }

    public async Task ApproveAsync(int id)
    {
        var comment = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Şərh tapılmadı.");

        comment.IsApproved = true;
        await _repo.SaveChangesAsync();
    }

    private static BlogCommentGetDto MapToDto(BlogComment c) => new()
    {
        Id = c.Id,
        Content = c.Content,
        Rating = c.Rating,
        UserName = c.User.FullName,
        UserImageUrl = c.User.ImageUrl,
        CreatedAt = c.CreatedAt,
        ParentCommentId = c.ParentCommentId,
        Replies = c.Replies.Select(MapToDto).ToList()
    };
}