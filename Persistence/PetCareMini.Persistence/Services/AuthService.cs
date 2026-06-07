using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Generators;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Auth;
using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;
using PetCareMini.Persistence.Contexts;
using PetCareMini.Persistence.Helpers;
namespace PetCareMini.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService;
    private readonly ILogger<AuthService> _logger;
    public AuthService(
    IUserRepository userRepository,
    IJwtTokenService jwtTokenService,
    AppDbContext context,
    IEmailService emailService,
    ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _context = context;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var emailExists = await _userRepository.IsEmailExistAsync(dto.Email);

        if (emailExists)
            throw new Exception("Email already exists");

       
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Role = UserRole.User,
            PasswordHash = PasswordHasher.HashPassword(dto.Password),
            RefreshToken = refreshToken,
            RefreshTokenExpireDate = DateTime.UtcNow.AddDays(7)
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        
        var confirmToken = Guid.NewGuid().ToString("N")[..8].ToUpper();
        user.EmailConfirmationToken = confirmToken;
        await _userRepository.SaveChangesAsync();

        try
        {
            await _emailService.SendEmailAsync(
                new List<string> { dto.Email },
                "Email Təsdiqi",
                $"<h2>Xoş gəldiniz, {dto.FullName}!</h2><p>Təsdiq kodunuz: <strong>{confirmToken}</strong></p>"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Email sending failed");
        }
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
        // Email təsdiqlənməyibsə, girişə icazə vermirik heleki test yoxla
        if (!user.IsEmailConfirmed)
            throw new InvalidOperationException("Email təsdiqlənməyib. Zəhmət olmasa emailinizi təsdiqləyin.");

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
    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email)
            ?? throw new KeyNotFoundException("Bu email ilə istifadəçi tapılmadı.");

        var token = Guid.NewGuid().ToString("N")[..8].ToUpper();

        user.PasswordResetToken = token;
        user.PasswordResetTokenExpireDate = DateTime.UtcNow.AddMinutes(15);
        await _context.SaveChangesAsync();

        await _emailService.SendEmailAsync(
            new List<string> { email },
            "Şifrə Sıfırlama",
            $"<h2>Şifrə Sıfırlama Kodu</h2><p>Kodunuz: <strong>{token}</strong></p><p>Bu kod 15 dəqiqə ərzində etibarlıdır.</p>"
        );
    }
    public async Task SendEmailConfirmationAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email)
            ?? throw new KeyNotFoundException("İstifadəçi tapılmadı.");

        if (user.IsEmailConfirmed)
            throw new InvalidOperationException("Email artıq təsdiqlənib.");

        var token = Guid.NewGuid().ToString("N")[..8].ToUpper();
        user.EmailConfirmationToken = token;
        await _context.SaveChangesAsync();

        await _emailService.SendEmailAsync(
            new List<string> { email },
            "Email Təsdiqi",
            $"<h2>Email Təsdiqi</h2><p>Təsdiq kodunuz: <strong>{token}</strong></p>"
        );
    }

    public async Task ConfirmEmailAsync(string email, string token)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.EmailConfirmationToken == token)
            ?? throw new InvalidOperationException("Token yanlışdır.");

        user.IsEmailConfirmed = true;
        user.EmailConfirmationToken = null;
        await _context.SaveChangesAsync();
    }
    public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email
                && u.PasswordResetToken == dto.Token
                && u.PasswordResetTokenExpireDate > DateTime.UtcNow)
            ?? throw new InvalidOperationException("Token yanlış və ya müddəti bitib.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpireDate = null;
        await _context.SaveChangesAsync();
    }

    public async Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenDto dto)
    {
       
        var user = await _userRepository.GetByRefreshTokenAsync(dto.RefreshToken);

        if (user is null)
            throw new KeyNotFoundException("Invalid refresh token.");

        
        if (user.RefreshTokenExpireDate < DateTime.UtcNow)
            throw new ArgumentException("Refresh token has expired. Please login again.");

        
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