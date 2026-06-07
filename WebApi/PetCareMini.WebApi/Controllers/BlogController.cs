using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Blog;
using PetCareMini.Persistence.Services;
using System.Security.Claims;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/blog")]
public class BlogController : ControllerBase
{
    private readonly IBlogPostService _service;
    private readonly IBlogCommentService _commentService;
    private readonly IBlogAuthorProfileService _profileService;

    public BlogController(
        IBlogPostService service,
        IBlogCommentService commentService,
        IBlogAuthorProfileService profileService)
    {
        _service = service;
        _commentService = commentService;
        _profileService = profileService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(string lang = "az")
    => Ok(new { data = await _service.GetAllPublishedAsync(lang), statusCode = 200 });

    [HttpGet("category/{slug}")]
    public async Task<IActionResult> GetByCategory(string slug, string lang = "az")
        => Ok(new { data = await _service.GetByCategoryAsync(slug, lang), statusCode = 200 });

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug, string lang = "az")
        => Ok(new { data = await _service.GetBySlugAsync(slug, lang), statusCode = 200 });

    [HttpGet("{postId}/comments")]
    public async Task<IActionResult> GetComments(int postId)
        => Ok(new { data = await _commentService.GetByPostIdAsync(postId), statusCode = 200 });

    [HttpGet("my")]
    [Authorize]
    public async Task<IActionResult> GetMyPosts([FromQuery] string lang = "az")
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.GetMyPostsAsync(userId, lang);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] BlogPostCreateDto dto)
    {
        var userId = GetUserId();
        await _service.CreateAsync(userId, dto);
        return StatusCode(201, new { message = "Post yaradıldı, admin təsdiqi gözlənilir.", statusCode = 201 });
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] BlogPostUpdateDto dto)
    {
        var userId = GetUserId();
        await _service.UpdateAsync(userId, id, dto);
        return Ok(new { message = "Post yeniləndi, admin təsdiqi gözlənilir.", statusCode = 200 });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();
        await _service.DeleteAsync(userId, id);
        return Ok(new { message = "Post silindi.", statusCode = 200 });
    }

    [HttpPost("{postId}/comments")]
    [Authorize]
    public async Task<IActionResult> AddComment(int postId, [FromBody] BlogCommentCreateDto dto)
    {
        var userId = GetUserId();
        dto.BlogPostId = postId;
        await _commentService.CreateAsync(userId, dto);
        return StatusCode(201, new { message = "Şərh əlavə edildi.", statusCode = 201 });
    }

    [HttpDelete("comments/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var userId = GetUserId();
        await _commentService.DeleteAsync(userId, id);
        return Ok(new { message = "Şərh silindi.", statusCode = 200 });
    }
    [HttpGet("author/profile")]
    [Authorize]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = GetUserId();
        var profile = await _profileService.GetByUserIdAsync(userId);
        return Ok(new { data = profile, statusCode = 200 });
    }

    [HttpPut("author/profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] BlogAuthorProfileDto dto)
    {
        var userId = GetUserId();
        await _profileService.CreateOrUpdateAsync(userId, dto);
        return Ok(new { message = "Profil yeniləndi.", statusCode = 200 });
    }


    [HttpGet("admin/all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAdmin()
        => Ok(new { data = await _service.GetAllAsync(), statusCode = 200 });

    [HttpGet("admin/pending")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPending()
        => Ok(new { data = await _service.GetPendingAsync(), statusCode = 200 });

    [HttpPatch("admin/{id}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Approve(int id)
    {
        await _service.ApproveAsync(id);
        return Ok(new { message = "Post təsdiqləndi.", statusCode = 200 });
    }

    [HttpPatch("admin/{id}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Reject(int id, [FromBody] BlogRejectDto dto)
    {
        await _service.RejectAsync(id, dto);
        return Ok(new { message = "Post rədd edildi.", statusCode = 200 });
    }

    [HttpPatch("admin/comments/{id}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveComment(int id)
    {
        await _commentService.ApproveAsync(id);
        return Ok(new { message = "Şərh təsdiqləndi.", statusCode = 200 });
    }


    private int GetUserId()
        => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}