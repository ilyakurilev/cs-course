using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Receiver
{
    public interface IReminderReceiver
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}
