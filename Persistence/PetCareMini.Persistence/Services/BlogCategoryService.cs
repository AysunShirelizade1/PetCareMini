using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Blog;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class BlogCategoryService : IBlogCategoryService
{
    private readonly IBlogCategoryRepository _repo;

    public BlogCategoryService(IBlogCategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<BlogCategoryGetDto>> GetAllAsync()
    {
        var categories = await _repo.GetAllAsync();
        return categories.Select(x => new BlogCategoryGetDto
        {
            Id = x.Id,
            Name = x.Name,
            Slug = x.Slug,
            Description = x.Description,
            IconUrl = x.IconUrl,
            PostCount = x.Posts.Count
        }).ToList();
    }

    public async Task<BlogCategoryGetDto> GetBySlugAsync(string slug)
    {
        var category = await _repo.GetBySlugAsync(slug)
            ?? throw new KeyNotFoundException($"Category '{slug}' tapılmadı.");

        return new BlogCategoryGetDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            IconUrl = category.IconUrl,
            PostCount = category.Posts.Count
        };
    }

    public async Task CreateAsync(BlogCategoryCreateDto dto)
    {
        var category = new BlogCategory
        {
            Name = dto.Name,
            Slug = GenerateSlug(dto.Name),
            Description = dto.Description,
            IconUrl = dto.IconUrl
        };

        await _repo.AddAsync(category);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Category {id} tapılmadı.");

        _repo.Delete(category);
        await _repo.SaveChangesAsync();
    }

    private static string GenerateSlug(string title)
        => title.ToLower()
            .Replace(" ", "-")
            .Replace("ə", "e").Replace("ı", "i")
            .Replace("ö", "o").Replace("ü", "u")
            .Replace("ç", "c").Replace("ş", "s")
            .Replace("ğ", "g");
}