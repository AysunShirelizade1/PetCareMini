namespace PetCareMini.Application.DTOs.Blog;

public class BlogCategoryCreateDto
{
    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }
    public string? IconUrl { get; set; }
}