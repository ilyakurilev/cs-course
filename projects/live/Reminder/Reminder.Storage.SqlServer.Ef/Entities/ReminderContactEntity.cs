using System;
using System.Collections.Generic;

namespace Reminder.Storage.SqlServer.Ef.Entities
{
    public class ReminderContactEntity
    {
        public Guid Id { get; set; }
        public string ChatId { get; set; }
        public ICollection<ReminderItemEntity> ReminderItems { get; set; }

        public ReminderContactEntity()
        {
            
        }

        public ReminderContactEntity(string chatId)
        {
            ChatId = chatId;
        }
    }
}