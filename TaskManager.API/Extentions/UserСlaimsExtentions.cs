using System;
using System.Security.Claims;
using TaskManager.Core;

namespace WebApplication3.Extentions;

public static class UserClaimsExtentions
{
    public static Guid UserId(this ClaimsPrincipal user)
    {
        return Guid.Parse(user.FindFirstValue(UserClaims.UserId) ?? string.Empty);
    }
}