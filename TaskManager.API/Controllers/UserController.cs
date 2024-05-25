using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.Service;
using WebApplication3.Contracts;

namespace Users.Api.Controllers;


[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService1)
    {
        _userService = userService1;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserLoginRequest request)
    {
        var token = await _userService.Login(request.Email, request.Password);

        ControllerContext.HttpContext.Response.Cookies.Append("token--cookies", token);

        return Ok(token);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequest request)
    {
        try
        {
            var user = await _userService.Register(request.UserName, request.Email, request.Password);
            return Ok(user);
        }
        catch (Exception e) when (e.Message is "user already exists")
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpGet("test")]
    public  IActionResult Test()
    {
        return Ok();
    }
}