using System.Text.Json;
using TaskManager.Core;

namespace TaskManager.Infastructure.Extentions;

public static class TaskLogExtentions
{
    public static string Serilize(this TaskLog taskLog)
    {
        return JsonSerializer.Serialize(taskLog);
    }
}