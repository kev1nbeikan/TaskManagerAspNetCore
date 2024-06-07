using NUnit.Framework;
using TaskManager.DataAccess;
using TaskManager.Tests.Utils;

namespace TaskManager.Tests.fixtures;

[SetUpFixture]
public static class TaskManagerFixture
{
    private const string ConnectionString = "Host=localhost;Database=taskManager-test;Username=postgres;Password=1";


    public static FakeDbContext context;

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        Console.WriteLine($"Используется строка подключения {ConnectionString}");
        context = FakeDbContext.Create(ConnectionString);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    [OneTimeTearDown]
    public static async void BaseTearDown()
    {
        await context.DisposeAsync();
    }
}