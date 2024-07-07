using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Core;
using TaskManager.DataAccess.Enities;

namespace TaskManager.DataAccess.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(MyTask.MAX_TITLE_LENGTH);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.UserId);
    }
}