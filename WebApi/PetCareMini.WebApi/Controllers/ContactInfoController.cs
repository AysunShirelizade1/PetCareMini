using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Contact;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/contact-info")]
public class ContactInfoController : ControllerBase
{
    private readonly IContactInfoService _service;

    public ContactInfoController(IContactInfoService service)
        => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetAsync();
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] ContactInfoDto dto)
    {
        await _service.UpdateAsync(dto);
        return Ok(new { message = "Contact info yeniləndi." });
    }
}