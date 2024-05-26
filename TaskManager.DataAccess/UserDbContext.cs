using Microsoft.EntityFrameworkCore;
using TaskManager.DataAccess.Configuration;
using UserEntity = TaskManager.DataAccess.Enities.UserEntity;

namespace User.DataAccess;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    public DbSet<UserEntity> Users { get; set; }
}