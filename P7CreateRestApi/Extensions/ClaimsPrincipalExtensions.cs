using System.Security.Claims;

namespace P7CreateRestApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        if (user is null) return string.Empty;

        var identity = (ClaimsIdentity?)user.Identity;

        if (identity is null) return string.Empty;

        IEnumerable<Claim> claims = identity.Claims;

        return claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
