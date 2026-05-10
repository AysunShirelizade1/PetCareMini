using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Product;
using Microsoft.AspNetCore.Authorization;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductQueryDto query)
    {
        return Ok(await _service.GetAllAsync(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, [FromQuery] string lang = "az")
    {
        var data = await _service.GetByIdAsync(id, lang);
        if (data is null) return NotFound();
        return Ok(data);
    }

    [HttpGet("{id}/recommended")]
    public async Task<IActionResult> GetRecommended(
        int id,
        [FromQuery] string lang = "az",
        [FromQuery] int count = 6)
    {
        var result = await _service.GetRecommendedAsync(id, lang, count);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(201, new { message = "Product created." });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        if (!result) return NotFound(new { message = "Product not found." });
        return Ok(new { message = "Product updated." });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound(new { message = "Product not found." });
        return Ok(new { message = "Product deleted." });
    }
}