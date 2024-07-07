namespace TaskManager.Core.Abstractions;

public interface INotifyHubLoggerService
{
    public Task<string?> Log(MyTask task, TaskAction action);
}