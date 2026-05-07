namespace PetCareMini.WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _service;

    public WishlistController(IWishlistService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetWishlist([FromQuery] string lang = "az")
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);
        var result = await _service.GetUserWishlistAsync(userId, lang);
        return Ok(result);
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> AddToWishlist(int productId)
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);
        var added = await _service.AddToWishlistAsync(userId, productId);

        if (!added)
            return Conflict(new { message = "Product already in wishlist." });

        return Ok(new { message = "Product added to wishlist." });
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> RemoveFromWishlist(int productId)
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);

        var removed = await _service.RemoveFromWishlistAsync(userId, productId);

        if (!removed)
            return NotFound(new { message = "Product not found in wishlist." });

        return Ok(new { message = "Product removed from wishlist." });
    }
}