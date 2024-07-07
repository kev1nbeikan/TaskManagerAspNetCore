namespace NotifyHub.Core.Abstractions;

public interface INotifyUserTasksRepo
{
    (TaskLog?, string?) GetLastLog(Guid userId);
}