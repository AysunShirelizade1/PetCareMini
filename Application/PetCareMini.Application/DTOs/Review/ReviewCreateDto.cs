
namespace PetCareMini.Application.DTOs.Review;

public class ReviewCreateDto
{
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int ProductId { get; set; }
}
