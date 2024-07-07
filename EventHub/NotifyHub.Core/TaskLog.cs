namespace NotifyHub.Core;

public class TaskLog
{
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
    public TaskAction Action { get; set; }
}