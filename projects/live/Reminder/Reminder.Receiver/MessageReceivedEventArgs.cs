using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Receiver
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public DateTimeOffset DateTime { get; }
        public string Message { get; }
        public string ContactId { get; }

        public MessageReceivedEventArgs(DateTimeOffset dateTime, string message, string contactId)
        {
            DateTime = dateTime;
            Message = message;
            ContactId = contactId;
        }
    }
}
