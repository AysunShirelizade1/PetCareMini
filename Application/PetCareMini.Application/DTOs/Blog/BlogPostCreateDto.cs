using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Blog;

public class BlogPostCreateDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string? CoverImageUrl { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; } = new();
}
