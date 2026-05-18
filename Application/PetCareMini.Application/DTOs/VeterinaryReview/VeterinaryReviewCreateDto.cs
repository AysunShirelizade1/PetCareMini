using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.VeterinaryReview;

public class VeterinaryReviewCreateDto
{
    public int VeterinarianId { get; set; }
    public int? ServiceId { get; set; }
    public int? AppointmentId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
}