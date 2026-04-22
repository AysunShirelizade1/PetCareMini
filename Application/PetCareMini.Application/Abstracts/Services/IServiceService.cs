using PetCareMini.Application.DTOs.Service;

namespace PetCareMini.Application.Abstracts.Services;

public interface IServiceService
{
    Task<List<ServiceGetDto>> GetAllAsync();
    Task<ServiceGetDto?> GetByIdAsync(int id);
    Task CreateAsync(ServiceCreateDto dto);
    Task<bool> UpdateAsync(int id, ServiceUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}