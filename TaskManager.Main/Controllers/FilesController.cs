using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication3.Controllers;

[Route("[controller]")]
public class FilesController : Controller
{
    private readonly ILogger<FilesController> _logger;

    public FilesController(ILogger<FilesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var response = HttpContext.Response;

        response.ContentType = "text/html; charset=utf-8";

        return View("upload");
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