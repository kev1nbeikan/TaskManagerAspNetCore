using Microsoft.Extensions.Caching.Distributed;
using TaskManager.Core;
using TaskManager.Core.Abstractions;

namespace TaskManager.DataAccess.Repositories;

public class TasksRepositoryWithCaching(TasksRepository tasksRepository, ICache cache) : ITaskRepository
{
    private readonly TasksRepository _tasksRepository = tasksRepository;
    private readonly ICache _cache = cache;


    private async Task<T> GetFromCacheOrDataBase<T>(string key, Func<Task<T>> getDataFromDb)
    {
        var cachedResult = await _cache.GetAsync<T>(key);
        if (cachedResult != null)
        {
            return cachedResult;
        }
        else
        {
            var result = await getDataFromDb();
            await _cache.SetAsync(key, result);
            return result;
        }
    }

    public async Task<List<MyTask>> GetAll()
    {
        return await GetFromCacheOrDataBase(nameof(GetAll), () => _tasksRepository.GetAll());
    }

    public async Task<Guid> Create(MyTask myTask)
    {
        return await GetFromCacheOrDataBase($"{nameof(Create)}_{myTask.Id}", () => _tasksRepository.Create(myTask));
    }


    public async Task<Guid> Update(MyTask myTask)
    {
        return await GetFromCacheOrDataBase($"{nameof(Update)}_{myTask.Id}", () => _tasksRepository.Update(myTask));
    }

    public async Task<Guid> Delete(Guid id)
    {
        return await GetFromCacheOrDataBase($"{nameof(Delete)}_{id}", () => _tasksRepository.Delete(id));
    }

    public async Task<(MyTask myTask, string Error)> Get(Guid id)
    {
        return await GetFromCacheOrDataBase($"{nameof(Get)}_{id}", () => _tasksRepository.Get(id));
    }

    public async Task<(MyTask myTask, string Error)> GetByUserAndTaskId(Guid id, Guid userId)
    {
        return await GetFromCacheOrDataBase($"{nameof(GetByUserAndTaskId)}_{id}_{userId}",
            () => _tasksRepository.GetByUserAndTaskId(id, userId));
    }

    public async Task<List<MyTask>> GetAllTaskByUserId(Guid userId)
        => await GetFromCacheOrDataBase($"{nameof(GetAllTaskByUserId)}_{userId}",
            () => _tasksRepository.GetAllTaskByUserId(userId));
}