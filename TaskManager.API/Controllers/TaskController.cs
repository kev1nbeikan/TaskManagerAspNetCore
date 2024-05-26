using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core;
using TaskManager.Core.Abstractions;
using WebApplication3.Contracts;
using WebApplication3.Extentions;

namespace WebApplication3.Controllers;

[Authorize]
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
        var task = await _taskService.GetTask(id, User.UserId());

        return task == null ? NotFound() : Ok(task);
    }


    [HttpGet]
    public async Task<ActionResult<List<TaskResponse>>> GetTasks()
    {
        var tasks = await _taskService.GetAllTaskByUserId(User.UserId());

        return Ok(tasks.Select(x =>
            new TaskResponse(x.Id, x.Title, x.Description, x.Status, x.CreatedDate, x.DueDate)));
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateTask(TaskRequest request)
    {
        var (myTask, error) = MyTask.Create(
            Guid.NewGuid(), request.Title, request.Description, request.Status, request.DueDate, DateTime.Now,
            User.UserId());
    
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
        var task = await _taskService.GetTask(id, User.UserId());

        if (task == null)
        {
            return NotFound();
        }

        var (myTask, error) = MyTask.Create(
            id, request.Title, request.Description, request.Status, DateTime.Now, request.DueDate, User.UserId());


        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }


        return Ok(await _taskService.Update(myTask));
    }


    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<Guid>> DeleteTask(Guid id)
    {
        var task = await _taskService.GetTask(id, User.UserId());

        if (task == null)
        {
            return NotFound();
        }

        return Ok(await _taskService.Delete(id));
    }
}