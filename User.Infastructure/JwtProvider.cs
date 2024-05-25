using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace User.Infastructure;

public class JwtProvider() : IJwtProvider
{

    private readonly IConfiguration _configuration;
    private string? secretKey;
    private int expiresHours;

    public JwtProvider(IConfiguration configuration) : this()
    {
        _configuration = configuration;
        
        secretKey = _configuration.GetSection("JwtOptions:SecretKey").Value;
        var value = _configuration.GetSection("JwtOptions:ExpiresHours").Value;
        if (value != null)
            expiresHours = int.Parse(value);
    }
    
    public string GenerateToken(Users.Core.User user)
    {
        Claim[] claims = new Claim[] { new Claim("userId", user.UserId.ToString()) }; // claims 

        var singningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials:
            singningCredentials,
            expires:
            DateTime.UtcNow.AddHours(expiresHours)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}