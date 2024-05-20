namespace TaskManager.Core.Abstractions;

public interface ITaskRepository
{
    Task<List<MyTask>> GetAll();
    Task<Guid> Create(MyTask myTask);
    Task<Guid> Update(MyTask myTask);
    Task<Guid> Delete(Guid id);
    Task<(MyTask myTask, string Error)> Get(Guid id);
}