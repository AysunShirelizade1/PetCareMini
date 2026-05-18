using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.VeterinaryReview;
using PetCareMini.Domain.Entities;
using System.Security.Claims;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/veterinary-reviews")]
public class VeterinaryReviewController : ControllerBase
{
    private readonly IVeterinaryReviewService _service;
    public VeterinaryReviewController(IVeterinaryReviewService service)
        => _service = service;

    // User - review yaz
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] VeterinaryReviewCreateDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _service.CreateAsync(userId, dto);
        return StatusCode(201, new { statusCode = 201 });
    }

    // Veterinarian detail page
    [HttpGet("veterinarian/{vetId}")]
    public async Task<IActionResult> GetByVeterinarian(int vetId, [FromQuery] string lang = "az")
    => Ok(new { data = await _service.GetByVeterinarianAsync(vetId, lang), statusCode = 200 });

    // Homepage testimonials
    [HttpGet("featured")]
    public async Task<IActionResult> GetFeatured()
        => Ok(new { data = await _service.GetFeaturedAsync(), statusCode = 200 });

    // About Us page
    [HttpGet("approved")]
    public async Task<IActionResult> GetAllApproved()
        => Ok(new { data = await _service.GetAllApprovedAsync(), statusCode = 200 });

    // Admin endpoints
    [HttpPatch("{id}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Approve(int id)
    {
        await _service.ApproveAsync(id);
        return Ok(new { statusCode = 200 });
    }

    [HttpPatch("{id}/featured")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SetFeatured(int id, [FromQuery] bool value)
    {
        await _service.SetFeaturedAsync(id, value);
        return Ok(new { statusCode = 200 });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { statusCode = 200 });
    }
}