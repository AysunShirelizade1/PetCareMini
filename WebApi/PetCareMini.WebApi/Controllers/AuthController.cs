using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Auth;
using System.Security.Claims;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result is null)
            return Unauthorized(new { message = "Email or password is incorrect" });

        return Ok(result);
    }

    // ✅ NEW ENDPOINT
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        // Get userId from JWT token claims
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                       ?? User.FindFirst("sub")
                       ?? User.FindFirst("userId");

        if (userIdClaim is null)
            return Unauthorized(new { message = "Invalid token." });

        var userId = int.Parse(userIdClaim.Value);
        var result = await _userService.GetMeAsync(userId);

        return Ok(result);
    }
}