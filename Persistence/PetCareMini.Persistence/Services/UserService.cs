using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.User;
using PetCareMini.Persistence.Contexts;
using PetCareMini.Persistence.Helpers;

namespace PetCareMini.Persistence.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public UserService(AppDbContext context, ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<UserGetDto?> GetMeAsync(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new KeyNotFoundException("User not found.");

        return new UserGetDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            ImageUrl = user.ImageUrl,
            Role = user.Role.ToString()
        };
    }

    public async Task UpdateProfileAsync(int userId, UserUpdateDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new KeyNotFoundException("User not found.");

        user.FullName = dto.FullName;
        user.PhoneNumber = dto.PhoneNumber;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAvatarAsync(int userId, IFormFile file)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new KeyNotFoundException("User not found.");

        var (imageUrl, _) = await _cloudinaryService.UploadImageAsync(file, "petcaremini/avatars");

        if (imageUrl is null)
            throw new Exception("Image upload failed.");

        user.ImageUrl = imageUrl;
        await _context.SaveChangesAsync();
    }

    public async Task ChangePasswordAsync(int userId, ChangePasswordDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new KeyNotFoundException("User not found.");

        if (!PasswordHasher.VerifyPassword(dto.CurrentPassword, user.PasswordHash))
            throw new UnauthorizedAccessException("Current password is incorrect.");

        user.PasswordHash = PasswordHasher.HashPassword(dto.NewPassword);
        await _context.SaveChangesAsync();
    }
}