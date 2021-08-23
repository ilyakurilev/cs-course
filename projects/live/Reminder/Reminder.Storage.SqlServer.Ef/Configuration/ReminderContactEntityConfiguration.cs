using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reminder.Storage.SqlServer.Ef.Entities;

namespace Reminder.Storage.SqlServer.Ef.Configuration
{
    public class ReminderContactEntityConfiguration : IEntityTypeConfiguration<ReminderContactEntity>
    {
        public void Configure(EntityTypeBuilder<ReminderContactEntity> builder)
        {
            builder.ToTable("ReminderContacts");
            
            builder.Property(_ => _.ChatId)
                .IsRequired()
                .HasMaxLength(32);

            builder.HasIndex(_ => _.ChatId)
                .IsUnique()
                .HasName("UQ_ReminderContacts_ChatId");
        }
    }
}