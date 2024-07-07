using Microsoft.Extensions.Logging;

namespace DependencyInjectionClasses;

public class ScopedUser : IScopedUser
{
    private IScoped scoped;
    private ILogger<ScopedUser> _logger;

    public ScopedUser(IScoped scoped, ILogger<ScopedUser> logger)
    {
        this.scoped = scoped;
        this._logger = logger;
        logger.LogInformation("ScopedUser created");
    }

    public void Increase()
    {
        _logger.LogInformation("ScopedUser increased");
        scoped.Increase();
    }
}

public interface IScopedUser
{
    public void Increase();
}