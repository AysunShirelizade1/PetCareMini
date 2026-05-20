using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.ContactMessage;
using System.Security.Claims;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactMessageService _service;

    public ContactController(IContactMessageService service)
    {
        _service = service;
    }

    // User — mesaj göndər
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Send([FromBody] ContactMessageCreateDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _service.SendMessageAsync(userId, dto);
        return Ok(new { message = "Mesajınız göndərildi." });
    }

    // User — öz mesajlarını gör
    [HttpGet("my")]
    [Authorize]
    public async Task<IActionResult> GetMyMessages()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.GetMyMessagesAsync(userId);
        return Ok(result);
    }

    // Admin — bütün mesajlar
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    // Admin — oxunmamışlar
    [HttpGet("unread")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUnread()
        => Ok(await _service.GetUnreadAsync());

    // Admin — arxivlənmişlər
    [HttpGet("archived")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetArchived()
        => Ok(await _service.GetArchivedAsync());

    // Admin — oxundu işarələ
    [HttpPatch("{id}/read")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        await _service.MarkAsReadAsync(id);
        return Ok();
    }

    // Admin — cavab ver
    [HttpPost("{id}/reply")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Reply(int id, [FromBody] ContactMessageReplyDto dto)
    {
        await _service.ReplyAsync(id, dto);
        return Ok(new { message = "Cavab göndərildi." });
    }

    // Admin — arxivləşdir
    [HttpPatch("{id}/archive")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Archive(int id)
    {
        await _service.ArchiveAsync(id);
        return Ok();
    }

    // Admin — sil
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}