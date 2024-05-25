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
    private string expiresHours;

    public JwtProvider(IConfiguration configuration) : this()
    {
        _configuration = configuration;
        
        secretKey = _configuration.GetSection("JwtOptions:SecretKey").Value;
        expiresHours = _configuration.GetSection("JwtOptions:ExpiresHours").Value;

    }
    
    public string GenerateToken(Users.Core.User user)
    {

        var singningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            signingCredentials:
            singningCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}