using Microsoft.Extensions.Configuration;

namespace User.Infastructure;

public class JwtOptions
{
    public string? SecretKey { get; set; }
    public int ExpiresHours { get; set; }
}