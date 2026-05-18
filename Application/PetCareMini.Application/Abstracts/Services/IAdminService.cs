using PetCareMini.Application.DTOs.Admin;

namespace PetCareMini.Application.Abstracts.Services;

public interface IAdminService
{
    Task<AdminStatisticsDto> GetStatisticsAsync();
}