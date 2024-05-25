using Microsoft.AspNetCore.Mvc;
using User.Application.Service;
using WebApplication3.Contracts;

namespace Users.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService1)
    {
        _userService = userService1;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Post(UserLoginRequest request)
    {
        var result = await _userService.Login(request.Email, request.Password);
        return Ok(result);
    }
    

    [HttpPost("register")]
    public async Task<IActionResult> Post(UserRegisterRequest request)
    {
        await _userService.Register(request.UserName, request.Email, request.Password);
        return Ok();
    }
}