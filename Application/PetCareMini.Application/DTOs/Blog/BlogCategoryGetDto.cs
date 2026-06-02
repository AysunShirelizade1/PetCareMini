using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Blog;

public class BlogCategoryGetDto
{
    public int Id { get; set; }
    public string NameAz { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string SlugAz { get; set; } = null!;
    public string SlugEn { get; set; } = null!;
    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }
    public string? IconUrl { get; set; }
    public int PostCount { get; set; }
}
