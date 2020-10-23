using Reminder.Storage;
using System;

namespace Reminder.Domain
{
    public class ReminderItemInfo
    {
        public Guid Id { get; }
        public DateTimeOffset DateTime { get; }
        public ReminderItemStatus Status { get; }
        public string Message { get; }
        public string ContactId { get; }

        public ReminderItemInfo(ReminderItem reminder)
        {
            Id = reminder.Id;
            DateTime = reminder.DateTime;
            Status = reminder.Status;
            Message = reminder.Message;
            ContactId = reminder.ContactId;
        }
    }
}