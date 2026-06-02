using PetCareMini.Application.DTOs.Admin;
using PetCareMini.Application.DTOs.User;

namespace PetCareMini.Application.Abstracts.Services;

public interface IAdminService
{
    Task<AdminStatisticsDto> GetStatisticsAsync();
    Task<List<UserGetDto>> GetAllUsersAsync();
}