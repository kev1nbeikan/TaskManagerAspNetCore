﻿using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TaskManager.DataAccess.Configuration;
using TaskManager.DataAccess.Enities;

namespace TaskManager.DataAccess;

public class TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<UserEntity> Users { get; set; }
}