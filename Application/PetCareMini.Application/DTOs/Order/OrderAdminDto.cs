using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Order;

public class OrderAdminDto
{
    public int Id { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? RejectReason { get; set; }   
    public DateTime CreatedAt { get; set; }
    public List<OrderItemGetDto> Items { get; set; } = new();
}