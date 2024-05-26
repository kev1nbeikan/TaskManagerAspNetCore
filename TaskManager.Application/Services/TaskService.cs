using TaskManager.Core;
using TaskManager.Core.Abstractions;

namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
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
        return await _taskRepository.Create(myTask);
    }

    public async Task<Guid> Update(MyTask myTask)
    {
        return await _taskRepository.Update(myTask);
    }

    public async Task<Guid> Delete(Guid id)
    {
        return await _taskRepository.Delete(id);
    }
}