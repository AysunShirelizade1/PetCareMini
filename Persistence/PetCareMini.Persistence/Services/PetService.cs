using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Pet;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepo;

    public PetService(IPetRepository petRepo)
    {
        _petRepo = petRepo;
    }

    public async Task<List<PetGetDto>> GetUserPetsAsync(int ownerId)
    {
        var pets = await _petRepo.GetUserPetsAsync(ownerId);

        return pets.Select(p => new PetGetDto
        {
            Id = p.Id,
            Name = p.Name,
            Age = p.Age,
            Gender = p.Gender,
            Type = p.Type,
            Breed = p.Breed,
            Weight = p.Weight,
            Notes = p.Notes
        }).ToList();
    }

    public async Task<PetGetDto> GetByIdAsync(int id, int ownerId)
    {
        var pet = await _petRepo.GetByIdAsync(id);

        // 404 if not found
        if (pet is null)
            throw new KeyNotFoundException($"Pet with id {id} not found.");

        // 403 if pet belongs to another user
        if (pet.OwnerId != ownerId)
            throw new UnauthorizedAccessException("You don't have access to this pet.");

        return new PetGetDto
        {
            Id = pet.Id,
            Name = pet.Name,
            Age = pet.Age,
            Gender = pet.Gender,
            Type = pet.Type,
            Breed = pet.Breed,
            Weight = pet.Weight,
            Notes = pet.Notes
        };
    }

    public async Task CreateAsync(int ownerId, PetCreateDto dto)
    {
        var pet = new Pet
        {
            Name = dto.Name,
            Age = dto.Age,
            Gender = dto.Gender,
            Type = dto.Type,
            Breed = dto.Breed,
            Weight = dto.Weight,
            Notes = dto.Notes,
            OwnerId = ownerId
        };

        await _petRepo.CreateAsync(pet);
        await _petRepo.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, int ownerId, PetUpdateDto dto)
    {
        var pet = await _petRepo.GetByIdAsync(id);

        if (pet is null)
            throw new KeyNotFoundException($"Pet with id {id} not found.");

        if (pet.OwnerId != ownerId)
            throw new UnauthorizedAccessException("You don't have access to this pet.");

        // Update fields
        pet.Name = dto.Name;
        pet.Age = dto.Age;
        pet.Gender = dto.Gender;
        pet.Type = dto.Type;
        pet.Breed = dto.Breed;
        pet.Weight = dto.Weight;
        pet.Notes = dto.Notes;

        await _petRepo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, int ownerId)
    {
        var pet = await _petRepo.GetByIdAsync(id);

        if (pet is null)
            throw new KeyNotFoundException($"Pet with id {id} not found.");

        if (pet.OwnerId != ownerId)
            throw new UnauthorizedAccessException("You don't have access to this pet.");

        _petRepo.Delete(pet);
        await _petRepo.SaveChangesAsync();
    }
}