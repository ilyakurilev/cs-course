using System;

namespace Reminder.Sender.Exceptions
{
    public class ReminderSenderException : Exception
    {
        public ReminderSenderException(Exception innerException) :
            base("Failed to send reminder notification", innerException)
        {
        }
    }
}
