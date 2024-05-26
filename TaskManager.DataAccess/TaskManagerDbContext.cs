using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TaskManager.DataAccess.Configuration;
using TaskManager.DataAccess.Enities;

namespace TaskManager.DataAccess;

public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
    }

    public DbSet<TaskEntity> Tasks { get; set; }
}