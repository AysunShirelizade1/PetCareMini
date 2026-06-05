using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Order;
using PetCareMini.Domain.Enums;
using System.Security.Claims;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll(
        [FromQuery] string lang = "az",
        [FromQuery] string? status = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _orderService.GetAllAsync(lang, status, page, pageSize);
        return Ok(result);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyOrders([FromQuery] string lang = "az")
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _orderService.GetMyOrdersAsync(userId, lang);
        return Ok(result);
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(
        [FromQuery] string lang = "az",
        [FromQuery] string? couponCode = null)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _orderService.CheckoutAsync(userId, lang, couponCode);
        return Ok(result);
    }

    [HttpPatch("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
    {
        await _orderService.UpdateStatusAsync(id, dto.Status);

        return Ok(new
        {
            message = "Order status updated successfully",
            status = dto.Status.ToString()
        });
    }

    [HttpPatch("{id}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RejectOrder(int id, [FromBody] RejectOrderDto dto)
    {
        await _orderService.RejectOrderAsync(id, dto.Reason);

        return Ok(new
        {
            message = "Order rejected successfully",
            reason = dto.Reason
        });
    }
}