using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Ai;
using PetCareMini.Infrastructure.Settings;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Infrastructure.Services;

public class AnthropicService : IAnthropicService
{
    private readonly HttpClient _httpClient;
    private readonly AnthropicSettings _settings;
    private readonly AppDbContext _context;

    public AnthropicService(
        HttpClient httpClient,
        IOptions<AnthropicSettings> options,
        AppDbContext context)
    {
        _httpClient = httpClient;
        _settings = options.Value;
        _context = context;
    }

    public async Task<string> AskAsync(string message, List<MessageDto>? history = null)
    {
        var products = await _context.Products
            .Where(p => p.IsActive)
            .Select(p => $"{p.NameAz} - {p.Price} AZN")
            .ToListAsync();

        var productContext = products.Any()
            ? "Platformanın mövcud məhsulları:\n" + string.Join("\n", products)
            : string.Empty;

        var systemPrompt = $"""
            Sən PetCareMini platformasının virtual veterinar assistentisən.
            Aşağıdakı mövzularda kömək edə bilərsən:
            - Ev heyvanlarının sağlamlığı və xəstəlikləri
            - Veterinar randevu tövsiyələri
            - Platformadakı xidmətlər və məhsullar haqqında məlumat
            Yalnız bu mövzularla bağlı suallara cavab ver. Qısa və aydın cavab ver.
            {productContext}
            """;

        var messages = new List<object>();

        if (history != null)
            foreach (var h in history)
                messages.Add(new { role = h.Role, content = h.Content });

        messages.Add(new { role = "user", content = message });

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);
        _httpClient.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

        var requestBody = new
        {
            model = _settings.Model,
            max_tokens = 1024,
            system = systemPrompt,
            messages
        };

        var response = await _httpClient.PostAsJsonAsync(
            "https://api.anthropic.com/v1/messages", requestBody);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        return result.GetProperty("content")[0].GetProperty("text").GetString()!;
    }
}