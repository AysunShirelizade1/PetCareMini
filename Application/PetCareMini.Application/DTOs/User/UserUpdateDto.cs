namespace PetCareMini.Application.DTOs.User;

public class UserUpdateDto
{
    public string FullName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
}