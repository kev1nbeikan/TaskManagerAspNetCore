using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TaskManager.Core.Abstractions;

namespace TaskManager.DataAccess.Repositories;

public class Cashe(IDistributedCache cache) : ICache
{
    private readonly IDistributedCache _cache = cache;

    public async Task SetAsync<T>(string key, T value) =>
        await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value));


    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _cache.GetStringAsync(key);

        return string.IsNullOrEmpty(value)
            ? default
            : JsonConvert.DeserializeObject<T>(value)!;
    }

    public Task RemoveAsync(string key) => _cache.RemoveAsync(key);
}