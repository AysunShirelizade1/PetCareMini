namespace PetCareMini.Application.DTOs.Blog;

public class BlogCategoryCreateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
}