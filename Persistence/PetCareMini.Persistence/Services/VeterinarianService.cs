using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Veterinarian;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class VeterinarianService : IVeterinarianService
{
    private readonly IVeterinarianRepository _vetRepository;

    public VeterinarianService(IVeterinarianRepository vetRepository)
    {
        _vetRepository = vetRepository;
    }

    public async Task<List<VeterinarianGetDto>> GetAllAsync()
    {
        var veterinarians = await _vetRepository.GetAllAsync();

        return veterinarians.Select(x => new VeterinarianGetDto
        {
            Id = x.Id,
            FullName = x.FullName,
            Specialty = x.Specialty,
            Bio = x.Bio,
            ProfileImageUrl = x.ProfileImageUrl,
            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            FacebookUrl = x.FacebookUrl,
            InstagramUrl = x.InstagramUrl,
            LinkedInUrl = x.LinkedInUrl,
            CreatedAt = x.CreatedAt
        }).ToList();
    }

    public async Task<VeterinarianGetDto?> GetByIdAsync(int id)
    {
        var veterinarian = await _vetRepository.GetByIdAsync(id);

        if (veterinarian is null)
            return null;

        return new VeterinarianGetDto
        {
            Id = veterinarian.Id,
            FullName = veterinarian.FullName,
            Specialty = veterinarian.Specialty,
            Bio = veterinarian.Bio,
            ProfileImageUrl = veterinarian.ProfileImageUrl,
            PhoneNumber = veterinarian.PhoneNumber,
            Email = veterinarian.Email,
            FacebookUrl = veterinarian.FacebookUrl,
            InstagramUrl = veterinarian.InstagramUrl,
            LinkedInUrl = veterinarian.LinkedInUrl,
            CreatedAt = veterinarian.CreatedAt
        };
    }

    public async Task CreateAsync(VeterinarianCreateDto dto)
    {
        var veterinarian = new Veterinarian
        {
            FullName = dto.FullName,
            Specialty = dto.Specialty,
            Bio = dto.Bio,
            ProfileImageUrl = dto.ProfileImageUrl,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            FacebookUrl = dto.FacebookUrl,
            InstagramUrl = dto.InstagramUrl,
            LinkedInUrl = dto.LinkedInUrl,
            CreatedAt = DateTime.UtcNow
        };

        await _vetRepository.AddAsync(veterinarian);
        await _vetRepository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, VeterinarianUpdateDto dto)
    {
        var veterinarian = await _vetRepository.GetByIdAsync(id);

        if (veterinarian is null)
            return false;

        veterinarian.FullName = dto.FullName;
        veterinarian.Specialty = dto.Specialty;
        veterinarian.Bio = dto.Bio;
        veterinarian.ProfileImageUrl = dto.ProfileImageUrl;
        veterinarian.PhoneNumber = dto.PhoneNumber;
        veterinarian.Email = dto.Email;
        veterinarian.FacebookUrl = dto.FacebookUrl;
        veterinarian.InstagramUrl = dto.InstagramUrl;
        veterinarian.LinkedInUrl = dto.LinkedInUrl;

        _vetRepository.Update(veterinarian);
        await _vetRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var veterinarian = await _vetRepository.GetByIdAsync(id);

        if (veterinarian is null)
            return false;

        _vetRepository.Delete(veterinarian);
        await _vetRepository.SaveChangesAsync();

        return true;
    }
}