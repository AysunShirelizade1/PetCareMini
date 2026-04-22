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

    public async Task<List<ServiceGetDto>> GetAllAsync()
    {
        var services = await _repository.GetAllAsync();

        return services.Select(x => new ServiceGetDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price
        }).ToList();
    }

    public async Task<ServiceGetDto?> GetByIdAsync(int id)
    {
        var service = await _repository.GetByIdAsync(id);

        if (service is null)
            return null;

        return new ServiceGetDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Price = service.Price
        };
    }

    public async Task CreateAsync(ServiceCreateDto dto)
    {
        var service = new Service
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price
        };

        await _repository.AddAsync(service);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, ServiceUpdateDto dto)
    {
        var service = await _repository.GetByIdAsync(id);

        if (service is null)
            return false;

        service.Name = dto.Name;
        service.Description = dto.Description;
        service.Price = dto.Price;

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
}