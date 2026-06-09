using PetCareMini.Application.DTOs.Ai;

namespace PetCareMini.Application.Abstracts.Services;

public interface IAnthropicService
{
    Task<string> AskAsync(string message, List<MessageDto>? history = null, string? productContext = null);
}