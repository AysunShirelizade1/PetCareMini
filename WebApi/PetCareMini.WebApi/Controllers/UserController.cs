using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.User;
using System.Security.Claims;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var result = await _userService.GetMeAsync(GetUserId());
        return Ok(result);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateDto dto)
    {
        await _userService.UpdateProfileAsync(GetUserId(), dto);
        return Ok("Profil yeniləndi.");
    }

    [HttpPut("avatar")]
    public async Task<IActionResult> UpdateAvatar(IFormFile file)
    {
        await _userService.UpdateAvatarAsync(GetUserId(), file);
        return Ok("Şəkil yeniləndi.");
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        await _userService.ChangePasswordAsync(GetUserId(), dto);
        return Ok("Şifrə dəyişdirildi.");
    }
}