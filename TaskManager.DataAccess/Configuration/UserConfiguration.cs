using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DataAccess.Enities;
using UserEntity = TaskManager.DataAccess.Enities.UserEntity;


namespace TaskManager.DataAccess.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(Users.Core.User.MAX_USERNA_LENGTH);
        builder.Property(x => x.PasswordHash).IsRequired();
    }
}