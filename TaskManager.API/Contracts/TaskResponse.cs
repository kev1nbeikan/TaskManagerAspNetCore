using TaskManager.Core;

namespace WebApplication3.Contracts;

public record TaskResponse(
    Guid Id,
    string Title,
    string Description,
    MyTaskStatus Status,
    DateTime CreatedDate,
    DateTime DueDate);

public record TaskRequest(
    
    string Title,
    string Description,
    MyTaskStatus Status,
    DateTime DueDate);