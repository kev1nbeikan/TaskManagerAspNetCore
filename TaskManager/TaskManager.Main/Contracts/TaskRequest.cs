using System;
using TaskManager.Core;

namespace WebApplication3.Contracts;

public record TaskRequest(
    string Title,
    string Description,
    MyTaskStatus Status,
    DateTime DueDate
);