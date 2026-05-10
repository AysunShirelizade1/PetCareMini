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
    public async Task<IActionResult> GetCart([FromQuery] string lang = "az")
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _service.GetCartAsync(userId, lang);
        return Ok(result);
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> AddToCart(int productId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _service.AddToCartAsync(userId, productId);
        return Ok(new { message = "Product added to cart." });
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> ChangeQuantity(
    int productId, [FromQuery] int quantity)
    {
        
        if (quantity <= 0)
            return BadRequest(new { message = "Quantity must be greater than 0." });

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _service.ChangeQuantityAsync(userId, productId, quantity);
        return Ok(new { message = "Quantity updated." });
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _service.RemoveFromCartAsync(userId, productId);
        return Ok(new { message = "Product removed from cart." });
    }
}