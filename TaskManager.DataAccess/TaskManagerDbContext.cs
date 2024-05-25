using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TaskManager.DataAccess.Enities;

namespace TaskManager.DataAccess;

public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
    {
    }

    public DbSet<TaskEntity> Tasks { get; set; }
}