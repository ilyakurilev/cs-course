using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Sender
{
    public interface IReminderSender
    {
        void Send(ReminderNotification notification);
    }
}
