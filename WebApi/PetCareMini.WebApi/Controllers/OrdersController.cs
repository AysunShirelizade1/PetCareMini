using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
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
    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders([FromQuery] string lang = "az")
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var data = await _orderService.GetMyOrdersAsync(userId, lang);

        return Ok(data);
    }
    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromQuery] string lang = "az")
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _orderService.CheckoutAsync(userId, lang);

        if (result is null)
            return BadRequest(new { message = "Cart is empty" });

        return Ok(new
        {
            message = "Checkout completed successfully",
            order = result
        });
    }

    
}