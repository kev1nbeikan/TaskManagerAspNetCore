using TaskManager.Core;

namespace TaskManager.Infastructure;

public interface INotifyHubLogger
{
    public Task<string?> Log(TaskLog taskLog);
}