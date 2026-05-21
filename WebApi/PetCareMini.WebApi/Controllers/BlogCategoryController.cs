using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Blog;

namespace PetCareMini.WebApi.Controllers;

[ApiController]
[Route("api/blog-categories")]
public class BlogCategoryController : ControllerBase
{
    private readonly IBlogCategoryService _service;

    public BlogCategoryController(IBlogCategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(new { data = await _service.GetAllAsync(), statusCode = 200 });

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
        => Ok(new { data = await _service.GetBySlugAsync(slug), statusCode = 200 });

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] BlogCategoryCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(201, new { message = "Kateqoriya yaradıldı.", statusCode = 201 });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { message = "Kateqoriya silindi.", statusCode = 200 });
    }
}