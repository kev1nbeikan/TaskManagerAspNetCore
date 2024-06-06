using TaskManager.Core;
using TaskManager.Core.Abstractions;
using TaskManager.Infastructure;

namespace TaskManager.Application.Services;

public class NotifyHubLoggerService: INotifyHubLoggerService 
{
    private readonly INotifyHubLogger _notifyHubLogger;

    public NotifyHubLoggerService(INotifyHubLogger notifyHubLogger)
    {
        _notifyHubLogger = notifyHubLogger;
    }

    public async Task<string?> Log(MyTask task, TaskAction action)
    {
        return await _notifyHubLogger.Log(new TaskLog
        {
            UserId = task.UserId,
            TaskId = task.Id,
            Action = action
        });
    }
}