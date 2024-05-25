using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace User.Infastructure;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateToken(Users.Core.User user)
    {
        Claim[] claims = new Claim[] { new Claim("userId", user.UserId.ToString()) }; // claims 

        var singningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials:
            singningCredentials,
            expires:
            DateTime.UtcNow.AddHours(_options.ExpiresHours)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}