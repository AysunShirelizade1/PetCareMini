using Microsoft.AspNetCore.Http;
using PetCareMini.Application.DTOs.User;

namespace PetCareMini.Application.Abstracts.Services;

public interface IUserService
{
    Task<UserGetDto?> GetMeAsync(int userId);
    Task UpdateProfileAsync(int userId, UserUpdateDto dto);
    Task UpdateAvatarAsync(int userId, IFormFile file);
    Task ChangePasswordAsync(int userId, ChangePasswordDto dto);
}