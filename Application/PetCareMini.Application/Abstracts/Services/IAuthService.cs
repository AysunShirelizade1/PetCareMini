using PetCareMini.Application.DTOs.Auth;

namespace PetCareMini.Application.Abstracts.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    
    Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenDto dto);
    Task ForgotPasswordAsync(string email);
    Task ResetPasswordAsync(ResetPasswordDto dto);
    Task SendEmailConfirmationAsync(string email);
    Task ConfirmEmailAsync(string email, string token);
}