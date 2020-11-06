using Reminder.Receiver;
using System;


namespace Reminder.Domain.Tests
{
    public class ReminderReceiver : IReminderReceiver
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public void SendMessage(DateTimeOffset dateTime, string message, string contactId)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(dateTime, message, contactId));
        }

    }
}
