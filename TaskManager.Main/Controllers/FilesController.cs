using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Application.Service;
using WebApplication3.Contracts;
using WebApplication3.Extentions;

namespace WebApplication3.Controllers;

[Route("[controller]")]
public class FilesController : Controller
{
    private readonly ILogger<FilesController> _logger;

    private readonly IUserService _userService;

    public FilesController(ILogger<FilesController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = HttpContext.Response;

        response.ContentType = "text/html; charset=utf-8";
        var user = await _userService.getUser(User.UserId());

        if (user == null)
        {
            user = Users.Core.User.Create(userId: Guid.Empty, userName: "Guest", email: "Guest", passwordHash: "Guest")
                .user;
        }

        return View("upload", user.ToUserRespone());
    }


    [HttpPost("upload")]
    public async Task<IActionResult> Post()
    {
        var request = HttpContext.Request;

        IFormFileCollection files = request.Form.Files;

        var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";


        Directory.CreateDirectory(uploadPath);

        foreach (var file in files)
        {
            // путь к папке uploads
            string fullPath = $"{uploadPath}/{file.FileName}";

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                _logger.LogInformation("Save file in path {path}", fullPath);
            }
        }

        return Ok("Файлы успешно загружены");
    }
}