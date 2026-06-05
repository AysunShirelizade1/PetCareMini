using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Order;

using PetCareMini.Domain.Enums;

public class UpdateOrderStatusDto
{
    public OrderStatus Status { get; set; }
}
