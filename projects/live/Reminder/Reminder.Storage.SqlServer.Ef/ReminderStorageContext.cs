using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reminder.Storage.SqlServer.Ef.Entities;

namespace Reminder.Storage.SqlServer.Ef
{
    public sealed class ReminderStorageContext : DbContext
    {
        public DbSet<ReminderItemEntity> ReminderItems { get; set; }
        public DbSet<ReminderContactEntity> ReminderContacts { get; set; }
        public DbSet<ReminderStatusEntity> ReminderStatuses { get; set; }

        public ReminderStorageContext(DbContextOptions<ReminderStorageContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => 
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        
    }
}
