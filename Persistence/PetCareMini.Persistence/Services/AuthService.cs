using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Auth;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Helpers;

namespace PetCareMini.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var emailExists = await _userRepository.IsEmailExistAsync(dto.Email);

        if (emailExists)
            throw new Exception("Email already exists");

        // ✅ Generate refresh token on register
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = PasswordHasher.HashPassword(dto.Password),
            RefreshToken = refreshToken,
            RefreshTokenExpireDate = DateTime.UtcNow.AddDays(7)
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        var token = _jwtTokenService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role.ToString()
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user is null)
            return null;

        var passwordIsCorrect = PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash);

        if (!passwordIsCorrect)
            return null;

        // ✅ Generate new refresh token on every login
        var refreshToken = _jwtTokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpireDate = DateTime.UtcNow.AddDays(7);
        await _userRepository.SaveChangesAsync();

        var token = _jwtTokenService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role.ToString()
        };
    }

    public async Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenDto dto)
    {
        // ✅ Find user by refresh token
        var user = await _userRepository.GetByRefreshTokenAsync(dto.RefreshToken);

        if (user is null)
            throw new KeyNotFoundException("Invalid refresh token.");

        // ✅ Check if refresh token is expired
        if (user.RefreshTokenExpireDate < DateTime.UtcNow)
            throw new ArgumentException("Refresh token has expired. Please login again.");

        // ✅ Generate new tokens
        var newAccessToken = _jwtTokenService.GenerateToken(user);
        var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpireDate = DateTime.UtcNow.AddDays(7);
        await _userRepository.SaveChangesAsync();

        return new AuthResponseDto
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role.ToString()
        };
    }
}