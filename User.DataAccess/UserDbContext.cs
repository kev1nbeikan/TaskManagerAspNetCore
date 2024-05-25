using Microsoft.EntityFrameworkCore;
using User.DataAccess.Entities;

namespace User.DataAccess;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
}