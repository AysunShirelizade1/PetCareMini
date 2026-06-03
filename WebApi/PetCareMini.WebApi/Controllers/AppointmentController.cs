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
    public async Task<IActionResult> GetAll(string lang = "az")
    {
        var result = await _appointmentService.GetAllAsync(lang);

        return Ok(new
        {
            data = result,
            statusCode = 200
        });
    }

    [Authorize(Roles = "Veterinarian")]
    [HttpGet("vet/my")]
    public async Task<IActionResult> GetMyVetAppointments([FromQuery] string lang = "az")
    {
        int vetUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _appointmentService.GetVetAppointmentsAsync(vetUserId, lang);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Veterinarian")]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] AppointmentStatusUpdateDto dto)
    {
        int? vetUserId = User.IsInRole("Veterinarian")
            ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            : null;

        await _appointmentService.UpdateStatusAsync(id, dto, vetUserId);
        return Ok(new { message = "Appointment status updated successfully." });
    }
}