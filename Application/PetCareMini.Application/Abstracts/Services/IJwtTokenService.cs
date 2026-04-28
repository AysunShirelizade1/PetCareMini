namespace PetCareMini.Application.Abstracts.Services;

using PetCareMini.Domain.Entities;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}