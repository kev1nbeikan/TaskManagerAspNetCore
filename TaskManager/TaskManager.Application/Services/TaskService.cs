using TaskManager.Core;
using TaskManager.Core.Abstractions;

namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly INotifyHubLoggerService _notifyHubLoggerService;

    public TaskService(ITaskRepository taskRepository, INotifyHubLoggerService notifyHubLoggerService)
    {
        _taskRepository = taskRepository;
        _notifyHubLoggerService = notifyHubLoggerService;
    }

    public async Task<List<MyTask>> GetAllTasks()
    {
        var tasks = await _taskRepository.GetAll();
        return tasks;
    }

    public Task<List<MyTask>> GetAllTaskByUserId(Guid userId)
    {
        return _taskRepository.GetAllTaskByUserId(userId);
    }

    public async Task<MyTask?> GetTask(Guid id, Guid userId)
    {
        var task = await _taskRepository.GetByUserAndTaskId(id, userId);
        return task.myTask;
    }

    public async Task<Guid> CreateTask(MyTask myTask)
    {
        await _notifyHubLoggerService.Log(myTask, TaskAction.Create);
        return await _taskRepository.Create(myTask);
    }

    public async Task<Guid> Update(MyTask myTask)
    {
        await _notifyHubLoggerService.Log(myTask, TaskAction.Update);
        return await _taskRepository.Update(myTask);
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _notifyHubLoggerService.Log(
            new MyTask(id, "", "", MyTaskStatus.InProgress, DateTime.Now, DateTime.Now,
                Guid.Empty), TaskAction.Delete);
        return await _taskRepository.Delete(id);
    }
}