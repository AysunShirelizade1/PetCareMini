using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using System.Security.Claims;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyWishlist()
    {
        var userId = GetUserId();

        var data = await _wishlistService.GetUserWishlistAsync(userId);

        return Ok(data);
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> AddToWishlist(int productId)
    {
        var userId = GetUserId();

        var result = await _wishlistService.AddToWishlistAsync(userId, productId);

        if (!result)
            return BadRequest(new { message = "Product already exists in wishlist" });

        return Ok(new { message = "Product added to wishlist" });
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> RemoveFromWishlist(int productId)
    {
        var userId = GetUserId();

        var result = await _wishlistService.RemoveFromWishlistAsync(userId, productId);

        if (!result)
            return NotFound(new { message = "Product not found in wishlist" });

        return Ok(new { message = "Product removed from wishlist" });
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}