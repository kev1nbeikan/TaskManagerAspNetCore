using Microsoft.VisualBasic;

namespace TaskManager.Core;

public class MyTask
{
    public const int MAX_TITLE_LENGTH = 255;
    public Guid Id { get; }

    public string Title { get; }

    public string Description { get; }

    public MyTaskStatus Status { get; }

    public DateTime CreatedDate { get; }

    public DateTime DueDate { get; }

    public Guid UserId { get; }

    private MyTask(Guid id, string title, string description, MyTaskStatus status, DateTime createdDate,
        DateTime dueDate, Guid userId)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
        CreatedDate = createdDate;
        DueDate = dueDate;
        UserId = userId;
    }

    public static (MyTask MyTask, string Error) Create(Guid id, string title, string description, MyTaskStatus status,
        DateTime createdDate, DateTime dueDate, Guid userId)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(title))
        {
            error = "Title cannot be empty";
        }


        if (userId == Guid.Empty) error = "User cannot be empty";


        var myTask = new MyTask(id, title, description, status, createdDate, dueDate, userId);

        return (myTask, error);
    }
}