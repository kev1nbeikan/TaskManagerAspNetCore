namespace Users.Core.Abstractions;

public class Result<T>
{
    public string? Error
    {
        get;
        set;
    }

    public T? Value { get; set; }
}