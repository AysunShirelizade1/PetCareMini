using PetCareMini.Application.DTOs.User;

namespace PetCareMini.Application.Abstracts.Services;

public interface IUserService
{
    Task<UserGetDto?> GetMeAsync(int userId);
}