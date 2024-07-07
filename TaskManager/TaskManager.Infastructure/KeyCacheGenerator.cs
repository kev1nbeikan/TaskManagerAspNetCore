namespace TaskManager.Infastructure;

public static class KeyCacheGenerator
{
    public static string GenerateKey(params string[] args)
    {
        return string.Join("_", args);
    }
}