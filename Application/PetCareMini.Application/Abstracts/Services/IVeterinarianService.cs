using PetCareMini.Application.DTOs.Veterinarian;

namespace PetCareMini.Application.Abstracts.Services;

public interface IVeterinarianService
{
    Task<List<VeterinarianGetDto>> GetAllAsync(string lang = "az");
    Task<VeterinarianGetDto?> GetByIdAsync(int id, string lang = "az");
    Task CreateAsync(VeterinarianCreateDto dto);
    Task<bool> UpdateAsync(int id, VeterinarianUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}