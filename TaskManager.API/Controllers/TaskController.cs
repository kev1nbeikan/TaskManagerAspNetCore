using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core;
using TaskManager.Core.Abstractions;
using WebApplication3.Contracts;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }


    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<MyTask>> GetTask(Guid id)
    {
        var task = await _taskService.GetTask(id);

        return Ok(task);
    }

    
    [HttpGet, Authorize]
    public async Task<ActionResult<List<TaskResponse>>> GetTasks()
    {
        var tasks = await _taskService.GetAllTasks();

        return Ok(tasks.Select(x =>
            new TaskResponse(x.Id, x.Title, x.Description, x.Status, x.CreatedDate, x.DueDate)));
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateTask(TaskRequest request)
    {
        var (myTask, error) = MyTask.Create(
            Guid.NewGuid(), request.Title, request.Description, request.Status, request.DueDate, DateTime.Now);
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var id = await _taskService.CreateTask(myTask);
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateTask(Guid id, TaskRequest request)
    {
        var (myTask, error) = MyTask.Create(
            id, request.Title, request.Description, request.Status, DateTime.Now, request.DueDate);
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        return Ok(await _taskService.Update(myTask));
    }


    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<Guid>> DeleteTask(Guid id)
    {
        return Ok(await _taskService.Delete(id));
    }
}