using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Appointment;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AppointmentCreateDto dto)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _appointmentService.CreateAsync(userId, dto);
        //  Fix: 201 instead of 200
        return StatusCode(201, new { message = "Appointment created successfully." });
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyAppointments([FromQuery] string lang = "az")
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //  Fix: lang parameter passed
        var result = await _appointmentService.GetUserAppointmentsAsync(userId, lang);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string lang = "az")
    {
        //  Fix: lang parameter passed
        var result = await _appointmentService.GetAllAsync(lang);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id, [FromBody] AppointmentStatusUpdateDto dto)
    {
        await _appointmentService.UpdateStatusAsync(id, dto);
        return Ok(new { message = "Appointment status updated successfully." });
    }
}