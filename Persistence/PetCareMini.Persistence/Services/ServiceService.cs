using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Service;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _repository;

    public ServiceService(IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ServiceGetDto>> GetAllAsync(string lang)
    {
        var services = await _repository.GetAllAsync();

        return services.Select(x => MapToDto(x, lang)).ToList();
    }

    public async Task<ServiceGetDto?> GetByIdAsync(int id, string lang)
    {
        var service = await _repository.GetByIdAsync(id);

        if (service is null)
            return null;

        return MapToDto(service, lang);
    }

    public async Task CreateAsync(ServiceCreateDto dto)
    {
        var service = new Service
        {
            NameAz = dto.NameAz,
            NameEn = dto.NameEn,
            DescriptionAz = dto.DescriptionAz,
            DescriptionEn = dto.DescriptionEn,
            Price = dto.Price,
            DurationMinutes = dto.DurationMinutes,
            ImageUrl = dto.ImageUrl,
            IsActive = true
        };

        await _repository.AddAsync(service);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, ServiceUpdateDto dto)
    {
        var service = await _repository.GetByIdAsync(id);

        if (service is null)
            return false;

        service.NameAz = dto.NameAz;
        service.NameEn = dto.NameEn;
        service.DescriptionAz = dto.DescriptionAz;
        service.DescriptionEn = dto.DescriptionEn;
        service.Price = dto.Price;
        service.DurationMinutes = dto.DurationMinutes;
        service.ImageUrl = dto.ImageUrl;
        service.IsActive = dto.IsActive;

        _repository.Update(service);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var service = await _repository.GetByIdAsync(id);

        if (service is null)
            return false;

        _repository.Delete(service);
        await _repository.SaveChangesAsync();

        return true;
    }

    private ServiceGetDto MapToDto(Service service, string lang)
    {
        var isEn = lang.ToLower() == "en";

        return new ServiceGetDto
        {
            Id = service.Id,
            Name = isEn ? service.NameEn : service.NameAz,
            Description = isEn ? service.DescriptionEn : service.DescriptionAz,
            Price = service.Price,
            DurationMinutes = service.DurationMinutes,
            ImageUrl = service.ImageUrl
        };
    }
}