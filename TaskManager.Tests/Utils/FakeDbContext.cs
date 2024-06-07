using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskManager.DataAccess;

namespace TaskManager.Tests.Utils;

public class FakeDbContext(DbContextOptions<TaskManagerDbContext> options) : TaskManagerDbContext(options)
{
    public static FakeDbContext Create(string connectionString)
    {
        var contextOptions = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseInMemoryDatabase("FakeDbContextTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        return new FakeDbContext(contextOptions);
    }
}