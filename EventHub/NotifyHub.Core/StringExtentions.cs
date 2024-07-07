using Newtonsoft.Json;

namespace NotifyHub.Core;

public static class StringExtentions
{
    public static TaskLog? DeserializeTaskLog(this string value)
    {
        return JsonConvert.DeserializeObject<TaskLog>(value);
    }
}