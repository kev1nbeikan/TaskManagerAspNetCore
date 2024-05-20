using Microsoft.Extensions.Logging;

namespace DependencyInjectionClasses;

public interface IDi
{
    void Increase();
}


public interface IScoped: IDi
{
}

public interface ISingleton: IDi
{
}

public interface ITransient: IDi
{
}

public class BaseDi<T>: IDi
{
    private readonly ILogger<T> _logger;
    private int i = 0;

    public BaseDi(ILogger<T> logger)
    {
        _logger = logger;
        _logger.LogInformation($"{GetType().Name} created i = {i}");
    }

    public void Increase()
    {
        i++;
        _logger.LogInformation($"{GetType().Name} increased i = {i}");

    }
}

public class Scoped : BaseDi<Scoped>, IScoped
{
    public Scoped(ILogger<Scoped> logger) : base(logger)
    {
    }
}

public class Singleton : BaseDi<Singleton>, ISingleton
{
    public Singleton(ILogger<Singleton> logger) : base(logger)
    {
    }
}

public class Transient : BaseDi<Transient>, ITransient
{
    public Transient(ILogger<Transient> logger) : base(logger)
    {
    }
}