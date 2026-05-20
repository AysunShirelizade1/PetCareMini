using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.VeterinaryReview;
using System.Security.Claims;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/veterinary-reviews")]
[Produces("application/json")]
public class VeterinaryReviewController : ControllerBase
{
    private readonly IVeterinaryReviewService _service;

    public VeterinaryReviewController(IVeterinaryReviewService service)
        => _service = service;

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] VeterinaryReviewCreateDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var created = await _service.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetByVeterinarian), new { vetId = dto.VeterinarianId }, new
        {
            statusCode = 201,
            data = created
        });
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var data = await _service.GetAllAsync(page, pageSize);
        return Ok(new { statusCode = 200, data });
    }

    [HttpGet("veterinarian/{vetId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByVeterinarian(int vetId, [FromQuery] string lang = "az")
    {
        var data = await _service.GetByVeterinarianAsync(vetId, lang);
        return Ok(new { statusCode = 200, data });
    }

    [HttpGet("featured")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeatured()
    {
        var data = await _service.GetFeaturedAsync();
        return Ok(new { statusCode = 200, data });
    }

    [HttpGet("approved")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllApproved()
    {
        var data = await _service.GetAllApprovedAsync();
        return Ok(new { statusCode = 200, data });
    }

    [HttpPatch("{id}/approve")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Approve(int id)
    {
        await _service.ApproveAsync(id);
        return Ok(new { statusCode = 200 });
    }

    [HttpPatch("{id}/featured")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SetFeatured(int id, [FromQuery] bool value)
    {
        await _service.SetFeaturedAsync(id, value);
        return Ok(new { statusCode = 200 });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { statusCode = 200 });
    }
}