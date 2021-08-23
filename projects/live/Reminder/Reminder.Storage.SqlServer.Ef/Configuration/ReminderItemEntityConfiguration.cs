using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reminder.Storage.SqlServer.Ef.Entities;

namespace Reminder.Storage.SqlServer.Ef.Configuration
{
    public class ReminderItemEntityConfiguration : IEntityTypeConfiguration<ReminderItemEntity>
    {
        public void Configure(EntityTypeBuilder<ReminderItemEntity> builder)
        {
            builder.ToTable("ReminderItems");

            builder.Property(_ => _.Id)
                .ValueGeneratedNever();
            
            builder.Property(_ => _.Message)
                .IsUnicode()
                .HasMaxLength(512);
            
            builder.Property(_ => _.DateTime)
                .IsRequired();
            
            builder.HasOne(_ => _.Status)
                .WithMany(_ => _.ReminderItems)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            
            builder.HasOne(_ => _.Contact)
                .WithMany(_ => _.ReminderItems)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}