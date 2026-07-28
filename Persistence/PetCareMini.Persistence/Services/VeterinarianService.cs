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

    public async Task<List<VeterinarianGetDto>> GetAllAsync(string lang = "az")
    {
        var veterinarians = await _vetRepository.GetAllAsync();

        return veterinarians.Select(x => new VeterinarianGetDto
        {
            Id = x.Id,
            FullName = x.FullName,
            Specialty = lang == "en" ? x.SpecialtyEn : x.SpecialtyAz,
            Bio = lang == "en" ? x.BioEn : x.BioAz,
            ProfileImageUrl = x.ProfileImageUrl,
            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            ExperienceYears = x.ExperienceYears,
            FacebookUrl = x.FacebookUrl,
            InstagramUrl = x.InstagramUrl,
            LinkedInUrl = x.LinkedInUrl,
            CreatedAt = x.CreatedAt
        }).ToList();
    }

    public async Task<VeterinarianGetDto?> GetByIdAsync(int id, string lang = "az")
    {
        var veterinarian = await _vetRepository.GetByIdAsync(id);

        if (veterinarian is null)
            return null;

        return new VeterinarianGetDto
        {
            Id = veterinarian.Id,
            FullName = veterinarian.FullName,
            Specialty = lang == "en" ? veterinarian.SpecialtyEn : veterinarian.SpecialtyAz,
            Bio = lang == "en" ? veterinarian.BioEn : veterinarian.BioAz,
            ProfileImageUrl = veterinarian.ProfileImageUrl,
            PhoneNumber = veterinarian.PhoneNumber,
            ExperienceYears = veterinarian.ExperienceYears,
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
            SpecialtyAz = dto.SpecialtyAz,
            SpecialtyEn = dto.SpecialtyEn,
            BioAz = dto.BioAz,
            BioEn = dto.BioEn,
            ProfileImageUrl = dto.ProfileImageUrl,
            PhoneNumber = dto.PhoneNumber,
            ExperienceYears = dto.ExperienceYears,
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
        veterinarian.SpecialtyAz = dto.SpecialtyAz;
        veterinarian.SpecialtyEn = dto.SpecialtyEn;
        veterinarian.BioAz = dto.BioAz;
        veterinarian.BioEn = dto.BioEn;
        veterinarian.ProfileImageUrl = dto.ProfileImageUrl;
        veterinarian.PhoneNumber = dto.PhoneNumber;
        veterinarian.ExperienceYears = dto.ExperienceYears;
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