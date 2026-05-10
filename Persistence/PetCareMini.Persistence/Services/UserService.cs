using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.User;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;  
    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserGetDto?> GetMeAsync(int userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            throw new KeyNotFoundException("User not found.");

        return new UserGetDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.ToString()
        };
    }
}