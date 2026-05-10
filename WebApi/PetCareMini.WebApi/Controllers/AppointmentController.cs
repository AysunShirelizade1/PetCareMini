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
    public async Task<IActionResult> Create(AppointmentCreateDto dto)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _appointmentService.CreateAsync(userId, dto);

        return Ok(new
        {
            message = "Appointment created successfully."
        });
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyAppointments()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _appointmentService
            .GetUserAppointmentsAsync(userId);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _appointmentService.GetAllAsync();

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        AppointmentStatusUpdateDto dto)
    {
        await _appointmentService.UpdateStatusAsync(id, dto);

        return Ok(new
        {
            message = "Appointment status updated successfully."
        });
    }
}