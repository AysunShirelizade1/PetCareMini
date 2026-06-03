using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Coupon;

public class CouponGetDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal DiscountPercent { get; set; }
    public bool IsActive { get; set; }
    public DateTime ExpireDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
