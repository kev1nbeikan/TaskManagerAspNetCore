using DependencyInjectionClasses;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]")]
public class DiController : ControllerBase
{
    private readonly IScoped scoped;
    private readonly ISingleton singleton;
    private readonly ITransient transient;
    private readonly IScopedUser scopedUser;

    public DiController(IScoped scoped, ISingleton singleton, ITransient transient, IScopedUser scopedUser)
    {
        this.scoped = scoped;
        this.singleton = singleton;
        this.transient = transient;
        this.scopedUser = scopedUser;
    }

    [HttpGet]
    public IActionResult Get()
    {
        scoped.Increase();
        singleton.Increase();
        transient.Increase();
        scopedUser.Increase();
        return Ok("Изучите вывод в консоль");
    }
}