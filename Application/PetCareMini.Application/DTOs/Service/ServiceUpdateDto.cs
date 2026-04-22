using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Service;

public class ServiceUpdateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}