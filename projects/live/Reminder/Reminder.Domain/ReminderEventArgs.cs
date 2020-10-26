using Reminder.Storage;
using System;

namespace Reminder.Domain
{
    public class ReminderEventArgs : EventArgs
    {
        public ReminderItemInfo Reminder { get; }

        public ReminderEventArgs(ReminderItem reminder)
        {
            Reminder = new ReminderItemInfo(reminder);
        }
    }
}
