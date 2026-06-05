using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Admin;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _service;

    public AdminController(IAdminService service) 
    {
        _service = service;
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var result = await _service.GetStatisticsAsync();
        return Ok(result);
    }
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _service.GetAllUsersAsync();
        return Ok(result);
    }
    [HttpPatch("users/{id}/role")]
    public async Task<IActionResult> ChangeRole(int id, [FromBody] ChangeUserRoleDto dto)
    {
        await _service.ChangeUserRoleAsync(id, dto.Role);
        return Ok(new { message = "User role updated successfully", role = dto.Role.ToString() });
    }

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _service.DeleteUserAsync(id);
        return Ok(new { message = "User deleted successfully" });
    }
}