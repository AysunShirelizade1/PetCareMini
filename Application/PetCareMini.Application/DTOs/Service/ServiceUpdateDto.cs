namespace PetCareMini.Application.DTOs.Service;

public class ServiceUpdateDto
{
    public string NameAz { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;

    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }

    public decimal Price { get; set; }

    public int? DurationMinutes { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;
}