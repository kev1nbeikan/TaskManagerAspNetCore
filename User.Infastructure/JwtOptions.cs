using Microsoft.Extensions.Configuration;

namespace User.Infastructure;

public class JwtOptions
{
    private readonly IConfiguration _configuration;

    public JwtOptions(IConfiguration configuration)
    {
        _configuration = configuration;
        SecretKey = _configuration.GetSection("JwtOptions:SecretKey").Value;
        var value = _configuration.GetSection("JwtOptions:ExpiresHours").Value;
        if (value != null)
            ExpiresHours = int.Parse(value);
    }

    public string? SecretKey { get; set; }
    public int ExpiresHours { get; set; }
}