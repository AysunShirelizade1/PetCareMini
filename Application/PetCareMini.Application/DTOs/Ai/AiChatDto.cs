namespace PetCareMini.Application.DTOs.Ai;

public class AiChatDto
{
    public string Message { get; set; } = string.Empty;
    public List<MessageDto>? History { get; set; }
}