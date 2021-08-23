using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reminder.Storage.SqlServer.Ef.Entities;

namespace Reminder.Storage.SqlServer.Ef.Configuration
{
    public class ReminderStatusEntityConfiguration : IEntityTypeConfiguration<ReminderStatusEntity>
    {
        public void Configure(EntityTypeBuilder<ReminderStatusEntity> builder)
        {
            builder.ToTable("ReminderStatuses");
            
            builder.Property(_ => _.Id)
                .ValueGeneratedNever();
            
            builder.Property(_ => _.Status)
                .IsRequired()
                .HasMaxLength(32);
            
            builder.HasIndex(_ => _.Status)
                .IsUnique()
                .HasName("UQ_ReminderStatuses_Status");
        }
    }
}