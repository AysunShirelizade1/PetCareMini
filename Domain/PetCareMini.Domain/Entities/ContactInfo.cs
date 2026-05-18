
using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class ContactInfo : BaseEntity
{
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? FacebookUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? TiktokUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? WhatsappUrl { get; set; }
    public string? YoutubeUrl { get; set; }
}
