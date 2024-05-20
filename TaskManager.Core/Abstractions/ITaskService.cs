namespace TaskManager.Core.Abstractions;

public interface ITaskService
{
    Task<List<MyTask>> GetAllTasks();
    Task<MyTask> GetTask(Guid id);
    Task<Guid> CreateTask(MyTask myTask);
    Task<Guid> Update(MyTask myTask);
    Task<Guid> Delete(Guid id);
}
