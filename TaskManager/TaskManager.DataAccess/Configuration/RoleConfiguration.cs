using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DataAccess.Enities;

namespace TaskManager.DataAccess.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(r => r.Id);


        builder.HasMany(r => r.Permissions)
            .WithMany(p => p.Roles);

    }
}