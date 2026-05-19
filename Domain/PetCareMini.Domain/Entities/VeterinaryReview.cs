using PetCareMini.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Domain.Entities;

public class VeterinaryReview : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int VeterinarianId { get; set; }
    public Veterinarian Veterinarian { get; set; } = null!;

    public int? ServiceId { get; set; }
    public Service? Service { get; set; }

    public int? AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }

    public int Rating { get; set; }         
    public string CommentAz { get; set; } = string.Empty!;
    public string CommentEn { get; set; } = string.Empty!;
    public bool IsApproved { get; set; } = false;  
    public bool IsFeatured { get; set; } = false;  
}
