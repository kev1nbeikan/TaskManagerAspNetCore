using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers;


[ApiController]
[Route("[controller]")]
public class ConfigurationTests: ControllerBase
{

    private readonly IConfiguration _configuration;

    public ConfigurationTests(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(_configuration.GetSection("JwtOptions:SecretKey").Value);
    }

}