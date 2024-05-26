using System.Security.Claims;
using TaskManager.Core;

namespace WebApplication3.Extentions;

public static class UserClaimsExtentions
{
    public static string? UserId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(UserClaims.UserId);
    }
}