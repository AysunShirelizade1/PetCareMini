namespace PetCareMini.WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _service.GetCartAsync(userId));
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> Add(int productId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _service.AddToCartAsync(userId, productId);

        return Ok("Added to cart");
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> Remove(int productId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _service.RemoveFromCartAsync(userId, productId);

        return Ok("Removed");
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> ChangeQuantity(int productId, int quantity)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _service.ChangeQuantityAsync(userId, productId, quantity);

        return Ok("Updated");
    }
}