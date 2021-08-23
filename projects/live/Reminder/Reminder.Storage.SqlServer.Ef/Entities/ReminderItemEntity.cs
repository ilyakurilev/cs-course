using System;

namespace Reminder.Storage.SqlServer.Ef.Entities
{
    public class ReminderItemEntity
    {
        public Guid Id { get; set; }
        public int StatusId { get; set; }
        public ReminderStatusEntity Status { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Message { get; set; }
        public ReminderContactEntity Contact { get; set; }

        
        public ReminderItemEntity()
        {
            
        }
        
        public ReminderItemEntity(ReminderItem item, ReminderContactEntity contact)
        {
            Id = item.Id;
            StatusId = (int) item.Status;
            DateTime = item.DateTime;
            Message = item.Message;
            Contact = contact;
        }

        public ReminderItem ToReminderItem() =>
            new ReminderItem(Id, (ReminderItemStatus) StatusId, DateTime, Message, Contact.ChatId);
    }
}