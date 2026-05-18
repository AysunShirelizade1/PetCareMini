using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Veterinarian;

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
    public async Task<IActionResult> GetAll()
    {
        var data = await _vetService.GetAllAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _vetService.GetByIdAsync(id);

        if (data is null)
            return NotFound();

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

        if (!result)
            return NotFound();

        return Ok(new { message = "Veterinarian updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _vetService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok(new { message = "Veterinarian deleted successfully" });
    }
}