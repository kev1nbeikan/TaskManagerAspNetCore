using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core;
using User.Application.Service;
using WebApplication3.Contracts;
using WebApplication3.Extentions;

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
    public async Task<ActionResult<string>> Login(UserLoginRequest request)
    {
        var (token, error) = await _userService.Login(request.Email, request.Password);

        if (!string.IsNullOrEmpty(error)) return BadRequest(error);

        ControllerContext.HttpContext.Response.Cookies.Append("token--cookies", token!);

        return Ok(token!);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequest request)
    {
        var (user, error) = await _userService.Register(request.UserName, request.Email, request.Password);

        return string.IsNullOrEmpty(error) ? Ok(user) : BadRequest(error);
    }

    [Authorize]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(User.UserId());
    }
}