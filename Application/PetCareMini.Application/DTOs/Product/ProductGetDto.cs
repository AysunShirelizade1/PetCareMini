using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Product;

public class ProductGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? ImageUrl { get; set; }

    public string CategoryName { get; set; } = null!;
}