using System.Security.Claims;

namespace PetCareMini.WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier)
                    ?? user.FindFirst("sub")
                    ?? user.FindFirst("userId");

        if (claim is null || !int.TryParse(claim.Value, out var userId))
            throw new UnauthorizedAccessException("İstifadəçi ID tapılmadı.");

        return userId;
    }
}