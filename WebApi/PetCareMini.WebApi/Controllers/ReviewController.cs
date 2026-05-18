namespace PetCareMini.WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Review;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IProductReviewService _service;

    public ReviewController(IProductReviewService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var created = await _service.CreateAsync(userId, dto);

        if (!created)
            return Conflict(new { message = "You have already reviewed this product." });

        return Ok(new { message = "Review submitted successfully." });
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetByProduct(int productId)
    {
        var reviews = await _service.GetProductReviewsAsync(productId);
        return Ok(reviews);
    }
}