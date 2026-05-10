using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Pet;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PetsController : ControllerBase
{
    private readonly IPetService _petService;

    public PetsController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyPets()
    {
        int ownerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _petService.GetUserPetsAsync(ownerId);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        int ownerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _petService.GetByIdAsync(id, ownerId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PetCreateDto dto)
    {
        int ownerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _petService.CreateAsync(ownerId, dto);

        return Ok(new
        {
            message = "Pet created successfully."
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PetUpdateDto dto)
    {
        int ownerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _petService.UpdateAsync(id, ownerId, dto);

        return Ok(new
        {
            message = "Pet updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int ownerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _petService.DeleteAsync(id, ownerId);

        return Ok(new
        {
            message = "Pet deleted successfully."
        });
    }
}