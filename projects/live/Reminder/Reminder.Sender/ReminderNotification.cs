using System;

namespace Reminder.Sender
{
    public class ReminderNotification
    {
        public DateTimeOffset DateTime { get; }
        public string Message { get; }
        public string ContactId { get; }

        public ReminderNotification(DateTimeOffset dateTime, string message, string contactId)
        {
            DateTime = dateTime;
            Message = message;
            ContactId = contactId;
        }
    }
}
