using Microsoft.AspNetCore.Mvc;
using NotifyHub.Core.Abstractions;

namespace EventNotifyHub.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskManagerNotifyController : ControllerBase
{
    private readonly ILogger<TaskManagerNotifyController> _logger;
    private readonly INotifyUserTasksRepo _notifyUserTasksRepo;

    public TaskManagerNotifyController(ILogger<TaskManagerNotifyController> logger,
        INotifyUserTasksRepo notifyUserTasksRepo)
    {
        _logger = logger;
        _notifyUserTasksRepo = notifyUserTasksRepo;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var (result, error) = _notifyUserTasksRepo.GetLastLog(Guid.Empty);

        if (string.IsNullOrEmpty(error)) return Ok(result);

        _logger.LogError(error);
        return BadRequest(error);
    }
}