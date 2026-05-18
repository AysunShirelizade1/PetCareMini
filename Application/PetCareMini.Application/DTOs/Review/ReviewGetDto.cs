namespace PetCareMini.Application.DTOs.Review;

public class ReviewGetDto
{
    public string UserName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string? UserImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }

}
