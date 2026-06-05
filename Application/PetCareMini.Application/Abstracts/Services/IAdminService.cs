using PetCareMini.Application.DTOs.Admin;
using PetCareMini.Application.DTOs.User;
using PetCareMini.Domain.Enums;

namespace PetCareMini.Application.Abstracts.Services;

public interface IAdminService
{
    Task<AdminStatisticsDto> GetStatisticsAsync();
    Task<List<UserGetDto>> GetAllUsersAsync();
    Task ChangeUserRoleAsync(int userId, UserRole role);
    Task DeleteUserAsync(int userId);

}