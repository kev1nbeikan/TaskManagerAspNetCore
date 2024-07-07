namespace TaskManager.Core.Abstractions;

public interface ICache
{
    Task SetAsync<T>(string key, T value);
    Task<T?> GetAsync<T>(string key);

    Task RemoveAsync(string key);
}