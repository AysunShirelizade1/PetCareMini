using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Pet;

public class PetGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Breed { get; set; }
    public decimal? Weight { get; set; }
    public string? Notes { get; set; }
}