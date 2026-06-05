using PetCareMini.Domain.Enums;

namespace PetCareMini.Application.DTOs.Admin;

public class ChangeUserRoleDto
{
    public UserRole Role { get; set; }
}