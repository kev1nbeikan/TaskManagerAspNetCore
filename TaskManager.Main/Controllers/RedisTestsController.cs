using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
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
        if (string.IsNullOrEmpty(result))
        {
            return NotFound();
        }

        var jsonObject = JsonConvert.DeserializeObject<RedisRequest>(result);

        return Ok(jsonObject);
    }


    [HttpPost]
    public async Task<IActionResult> Post(RedisRequest request)
    {
        await cache.SetStringAsync(request.Key, JsonConvert.SerializeObject(request));
        return Ok();
    }
}