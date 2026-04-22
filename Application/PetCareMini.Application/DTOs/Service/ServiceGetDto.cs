using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Service;

public class ServiceGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
