using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Faq;

namespace PetCareMini.WebApi.Controllers;

[ApiController]

[Route("api/[controller]")]
public class FaqsController : ControllerBase
{
    private readonly IFaqService _faqService;
    public FaqsController(IFaqService faqService)
    {
        _faqService = faqService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string lang = "az")
    {
        var data = await _faqService.GetAllAsync(lang);
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, [FromQuery] string lang = "az")
    {
        var data = await _faqService.GetByIdAsync(id, lang);

        if (data is null)
            return NotFound();

        return Ok(data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FaqCreateDto dto)
    {
        await _faqService.CreateAsync(dto);
        return Ok(new { message = "FAQ created successfully" });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] FaqUpdateDto dto)
    {
        var result = await _faqService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return Ok(new { message = "FAQ updated successfully" });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _faqService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok(new { message = "FAQ deleted successfully" });
    }
}

// This controller provides endpoints for managing FAQs
// (Frequently Asked Questions) in the PetCareMini application.
// It includes methods for retrieving all FAQs, retrieving a specific FAQ by ID,
// creating a new FAQ, updating an existing FAQ, and deleting an FAQ.
// The create, update, and delete operations are restricted to users with the "Admin" role.

