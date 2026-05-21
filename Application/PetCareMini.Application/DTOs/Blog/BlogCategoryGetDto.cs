using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Blog;

public class BlogCategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public int PostCount { get; set; }
}
