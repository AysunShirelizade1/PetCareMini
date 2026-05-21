using PetCareMini.Application.DTOs.Blog;

namespace PetCareMini.Application.Abstracts.Services;

public interface IBlogCommentService
{
    Task<List<BlogCommentGetDto>> GetByPostIdAsync(int postId);
    Task CreateAsync(int userId, BlogCommentCreateDto dto);
    Task DeleteAsync(int userId, int id);           
    Task ApproveAsync(int id);                      
}