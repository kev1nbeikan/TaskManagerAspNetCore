using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using WebApplication3.Contracts;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]")]
public class RedisTestsController(IDistributedCache cache) : ControllerBase
{
    [HttpGet("{key}")]
    public async Task<IActionResult> Get(string key)
    {
        var result = await cache.GetStringAsync(key);
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Get(RedisRequest request)
    {
        await cache.SetStringAsync(request.Key, request.Value.ToString());
        return Ok();
    }
}