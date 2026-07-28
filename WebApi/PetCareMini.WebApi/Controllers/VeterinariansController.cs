using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Appointment;
using PetCareMini.Application.DTOs.Veterinarian;
using PetCareMini.Persistence.Services;
using System.Security.Claims;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VeterinariansController : ControllerBase
{
    private readonly IVeterinarianService _vetService;

    public VeterinariansController(IVeterinarianService vetService)
    {
        _vetService = vetService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string lang = "az")
    {
        var data = await _vetService.GetAllAsync(lang);
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, [FromQuery] string lang = "az")
    {
        var data = await _vetService.GetByIdAsync(id, lang);
        if (data is null) return NotFound();
        return Ok(data);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] VeterinarianCreateDto dto)
    {
        await _vetService.CreateAsync(dto);
        return Created(string.Empty, new { message = "Veterinarian created successfully" });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] VeterinarianUpdateDto dto)
    {
        var result = await _vetService.UpdateAsync(id, dto);
        if (!result) return NotFound();
        return Ok(new { message = "Veterinarian updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _vetService.DeleteAsync(id);
        if (!result) return NotFound();
        return Ok(new { message = "Veterinarian deleted successfully" });
    }
}