using Microsoft.Extensions.Caching.Distributed;
using TaskManager.Core;
using TaskManager.Core.Abstractions;
using TaskManager.Infastructure;

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

    private async Task ClearCacheWithTask(MyTask myTask)
    {
        await _cache.RemoveAsync(KeyCacheGenerator.GenerateKey(nameof(GetAllTaskByUserId), myTask.UserId.ToString()));
        await _cache.RemoveAsync(KeyCacheGenerator.GenerateKey(nameof(GetByUserAndTaskId), myTask.Id.ToString(),
            myTask.UserId.ToString()));
        await _cache.RemoveAsync(KeyCacheGenerator.GenerateKey(nameof(Get), myTask.Id.ToString()));
        await _cache.RemoveAsync(KeyCacheGenerator.GenerateKey(nameof(GetAll)));
    }

    public async Task<List<MyTask>> GetAll()
    {
        return await GetFromCacheOrDataBase(nameof(GetAll), () => _tasksRepository.GetAll());
    }

    public async Task<Guid> Create(MyTask myTask)
    {
        var result = await _tasksRepository.Create(myTask);
        if (result == Guid.Empty) return result;

        await ClearCacheWithTask(myTask);

        return result;
    }


    public async Task<Guid> Update(MyTask myTask)
    {
        var result = await _tasksRepository.Update(myTask);
        if (result == Guid.Empty) return result;

        await ClearCacheWithTask(myTask);

        return result;
    }

    public async Task<Guid> Delete(Guid id)
    {
        var task = await _tasksRepository.Get(id);

        var result = await _tasksRepository.Delete(id);
        if (result == Guid.Empty) return result;

        await ClearCacheWithTask(task.myTask);

        return result;
    }

    public async Task<(MyTask myTask, string Error)> Get(Guid id)
    {
        return await GetFromCacheOrDataBase($"{nameof(Get)}_{id}", () => _tasksRepository.Get(id));
    }

    public async Task<(MyTask myTask, string Error)> GetByUserAndTaskId(Guid id, Guid userId)
    {
        return await GetFromCacheOrDataBase(
            KeyCacheGenerator.GenerateKey(nameof(GetByUserAndTaskId), id.ToString(), userId.ToString()),
            () => _tasksRepository.GetByUserAndTaskId(id, userId));
    }

    public async Task<List<MyTask>> GetAllTaskByUserId(Guid userId)
        => await GetFromCacheOrDataBase(
            KeyCacheGenerator.GenerateKey(nameof(GetAllTaskByUserId), userId.ToString()),
            () => _tasksRepository.GetAllTaskByUserId(userId));
}