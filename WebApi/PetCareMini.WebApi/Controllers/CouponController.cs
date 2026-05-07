using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Coupon;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouponController : ControllerBase
{
    private readonly ICouponService _service;

    public CouponController(ICouponService service)
    {
        _service = service;
    }

    // User kupon tətbiq edir — cart məbləğini görür
    [HttpPost("apply")]
    [Authorize]
    public async Task<IActionResult> Apply([FromBody] CouponApplyDto dto)
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);
        var result = await _service.ApplyAsync(userId, dto.Code);
        return Ok(result);
    }

    // Admin yeni kupon yaradır
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCouponDto dto)
    {
        await _service.CreateAsync(dto.Code, dto.DiscountPercent, dto.ExpireDate);
        return StatusCode(201, new { message = "Coupon created." });
    }

    // Admin kuponu deaktiv edir
    [HttpPatch("{id}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deactivate(int id)
    {
        await _service.DeactivateAsync(id);
        return Ok(new { message = "Coupon deactivated." });
    }
}