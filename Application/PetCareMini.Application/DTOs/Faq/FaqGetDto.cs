namespace PetCareMini.Application.DTOs.Faq;

public class FaqGetDto
{
    public int Id { get; set; }

    public string Question { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;
}