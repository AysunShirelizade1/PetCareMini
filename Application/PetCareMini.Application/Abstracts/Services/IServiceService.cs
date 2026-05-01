using PetCareMini.Application.DTOs.Service;

namespace PetCareMini.Application.Abstracts.Services;

public interface IServiceService
{
    Task<List<ServiceGetDto>> GetAllAsync(string lang);

    Task<ServiceGetDto?> GetByIdAsync(int id, string lang);

    Task CreateAsync(ServiceCreateDto dto);

    Task<bool> UpdateAsync(int id, ServiceUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}