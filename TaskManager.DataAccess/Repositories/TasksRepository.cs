using Microsoft.EntityFrameworkCore;
using TaskManager.Core;
using TaskManager.Core.Abstractions;
using TaskManager.DataAccess.Enities;

namespace TaskManager.DataAccess.Repositories;

public class TasksRepository : ITaskRepository
{
    private readonly TaskManagerDbContext _dbContext;
    
    public TasksRepository(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MyTask>> GetAll()
    {
        var tasksEntities = await _dbContext.Tasks.AsNoTracking().ToListAsync();
        return tasksEntities
            .Select(x => MyTask.Create(x.Id, x.Title, x.Description, x.Status, x.CreatedDate, x.DueDate, x.UserId)
                .MyTask)
            .ToList();
    }

    public async Task<Guid> Create(MyTask myTask)
    {
        var taskEntity = new TaskEntity
        {
            Id = myTask.Id,
            Title = myTask.Title,
            Description = myTask.Description,
            Status = myTask.Status,
            CreatedDate = myTask.CreatedDate,
            DueDate = myTask.DueDate,
            UserId = myTask.UserId
        };
        await _dbContext.Tasks.AddAsync(taskEntity);
        await _dbContext.SaveChangesAsync();
        return taskEntity.Id;
    }

    public async Task<Guid> Update(MyTask myTask)
    {
        await _dbContext.Tasks.Where(x => x.Id == myTask.Id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(x => x.Title, x => myTask.Title).SetProperty(x => x.Description, x => myTask.Description)
                    .SetProperty(x => x.Status, x => myTask.Status)
                    .SetProperty(x => x.CreatedDate, x => myTask.CreatedDate)
                    .SetProperty(x => x.DueDate, x => myTask.DueDate));
        return myTask.Id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _dbContext.Tasks.Where(x => x.Id == id).ExecuteDeleteAsync();
        return id;
    }

    public async Task<(MyTask myTask, string Error)> Get(Guid id)
    {
        var taskEntity = await _dbContext.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        return taskEntity is null
            ? (null!, "Task not found")
            : MyTask.Create(taskEntity.Id, taskEntity.Title, taskEntity.Description, taskEntity.Status,
                taskEntity.CreatedDate, taskEntity.DueDate, taskEntity.UserId);
    }

    public async Task<(MyTask myTask, string Error)> GetByUserAndTaskId(Guid id, Guid userId)
    {
        var taskEntity = await _dbContext.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        return taskEntity is null
            ? (null!, "Task not found")
            : MyTask.Create(taskEntity.Id, taskEntity.Title, taskEntity.Description, taskEntity.Status,
                taskEntity.CreatedDate, taskEntity.DueDate, taskEntity.UserId);
    }

    public async Task<List<MyTask>> GetAllTaskByUserId(Guid userId)
    {
        var tasksEntities = await _dbContext.Tasks.AsNoTracking().ToListAsync();
        return tasksEntities.Where(x => x.UserId == userId)
            .Select(x => MyTask.Create(x.Id, x.Title, x.Description, x.Status, x.CreatedDate, x.DueDate, x.UserId)
                .MyTask)
            .ToList();
    }
}