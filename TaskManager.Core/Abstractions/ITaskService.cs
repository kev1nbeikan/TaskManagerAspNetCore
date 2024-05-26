namespace TaskManager.Core.Abstractions;

public interface ITaskService
{
    Task<List<MyTask>> GetAllTasks();
    
    Task<List<MyTask>> GetAllTaskByUserId(Guid userId);
    Task<MyTask?> GetTask(Guid id, Guid userId);
    Task<Guid> CreateTask(MyTask myTask);
    Task<Guid> Update(MyTask myTask);
    Task<Guid> Delete(Guid id);
}
