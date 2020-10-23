using Reminder.Storage;

namespace Reminder.Domain
{
    public class ReminderSentEventArgs
    {
        public ReminderItemInfo Reminder { get; }

        public ReminderSentEventArgs(ReminderItem reminder)
        {
            Reminder = new ReminderItemInfo(reminder);
        }
    }
}
