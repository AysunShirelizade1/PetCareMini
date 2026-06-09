using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Ai;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AiController : ControllerBase
{
    private readonly IAnthropicService _anthropicService;
    private readonly AppDbContext _context;

    public AiController(IAnthropicService anthropicService, AppDbContext context)
    {
        _anthropicService = anthropicService;
        _context = context;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Chat([FromBody] AiChatDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Message))
            return BadRequest(new { message = "Mesaj boş ola bilməz." });

        var products = await _context.Products
            .Where(p => p.IsActive)
            .Select(p => $"{p.NameAz} - {p.Price} AZN")
            .ToListAsync();

        var productContext = products.Any()
            ? "Platformanın mövcud məhsulları:\n" + string.Join("\n", products)
            : null;

        var reply = await _anthropicService.AskAsync(dto.Message, dto.History, productContext);
        return Ok(new AiChatResponseDto { Reply = reply });
    }
}