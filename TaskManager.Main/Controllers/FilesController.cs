using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Abstractions;
using User.Application.Service;
using WebApplication3.Contracts;
using WebApplication3.Extentions;

namespace WebApplication3.Controllers;

[Route("[controller]")]
public class FilesController : Controller
{
    private readonly ILogger<FilesController> _logger;

    private readonly IUserService _userService;
    private readonly IFileService _fileService;

    public FilesController(ILogger<FilesController> logger, IUserService userService, IFileService fileService)
    {
        _logger = logger;
        _userService = userService;
        _fileService = fileService;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = HttpContext.Response;

        response.ContentType = "text/html; charset=utf-8";
        var user = await _userService.getUser(User.UserId()) ??
                   Users.Core.User.Create(userId: Guid.Empty, userName: "Guest", email: "Guest", passwordHash: "Guest")
                       .user;

        return View("upload", user!.ToUserRespone());
    }


    [HttpPost("upload")]
    public async Task<IActionResult> Post()
    {
        var request = HttpContext.Request;

        IFormFileCollection files = request.Form.Files;

        _fileService.SetPathUpload($"{Directory.GetCurrentDirectory()}/uploads");

        foreach (var file in files)
        {
            await _fileService.SaveFile(file);
            _logger.LogInformation("Save file in path");
        }

        return Ok("Файлы успешно загружены");
    }
}