using System.Collections.Generic;

namespace Reminder.Storage.SqlServer.Ef.Entities
{
    public class ReminderStatusEntity
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public ICollection<ReminderItemEntity> ReminderItems { get; set; }
    }
}