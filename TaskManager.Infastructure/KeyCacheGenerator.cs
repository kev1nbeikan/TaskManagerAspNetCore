namespace TaskManager.Infastructure;

public static class KeyCacheGenerator
{
    public static string GenerateKey(params string[] args)
    {
        var key = string.Join("_", args);
        return key;
    }
}