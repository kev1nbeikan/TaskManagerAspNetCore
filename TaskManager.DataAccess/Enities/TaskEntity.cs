using TaskManager.Core;

namespace TaskManager.DataAccess.Enities;

public class TaskEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public MyTaskStatus Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime DueDate { get; set; }
}