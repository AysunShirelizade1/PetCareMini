using PetCareMini.Application.DTOs.Faq;

namespace PetCareMini.Application.Abstracts.Services;

public interface IFaqService
{
    Task<List<FaqGetDto>> GetAllAsync(string lang);

    Task<FaqGetDto?> GetByIdAsync(int id, string lang);

    Task CreateAsync(FaqCreateDto dto);

    Task<bool> UpdateAsync(int id, FaqUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}
