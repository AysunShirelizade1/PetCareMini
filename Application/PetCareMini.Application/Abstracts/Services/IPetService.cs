using PetCareMini.Application.DTOs.Pet;

namespace PetCareMini.Application.Abstracts.Services;

public interface IPetService
{
    Task<List<PetGetDto>> GetUserPetsAsync(int ownerId);
    Task<PetGetDto> GetByIdAsync(int id, int ownerId);
    Task CreateAsync(int ownerId, PetCreateDto dto);
    Task UpdateAsync(int id, int ownerId, PetUpdateDto dto);
    Task DeleteAsync(int id, int ownerId);
}