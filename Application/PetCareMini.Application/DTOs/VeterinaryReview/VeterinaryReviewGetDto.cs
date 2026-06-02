using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.VeterinaryReview;

public class VeterinaryReviewGetDto
{
    public int Id { get; set; }
    public string UserFullName { get; set; } = null!;
    public string VeterinarianName { get; set; } = null!;
    public string? ServiceName { get; set; }
    public int Rating { get; set; }
    public string CommentAz { get; set; } = string.Empty!;
    public string CommentEn { get; set; } = string.Empty!;
    public bool IsFeatured { get; set; }
    public bool IsApproved { get; set; }
    public DateTime CreatedAt { get; set; }
}