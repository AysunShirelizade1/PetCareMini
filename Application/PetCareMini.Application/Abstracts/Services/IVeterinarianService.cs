using PetCareMini.Application.DTOs.Veterinarian;

namespace PetCareMini.Application.Abstracts.Services;

public interface IVeterinarianService
{
    Task<List<VeterinarianGetDto>> GetAllAsync();
    Task<VeterinarianGetDto?> GetByIdAsync(int id);
    Task CreateAsync(VeterinarianCreateDto dto);
    Task<bool> UpdateAsync(int id, VeterinarianUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}