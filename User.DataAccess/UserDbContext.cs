using Microsoft.EntityFrameworkCore;
using User.DataAccess.Configuration;
using User.DataAccess.Entities;

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