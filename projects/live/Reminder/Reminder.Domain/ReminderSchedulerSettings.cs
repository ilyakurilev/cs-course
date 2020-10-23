using System;

namespace Reminder.Domain
{
    public class ReminderSchedulerSettings
    {
        public TimeSpan TimerDelay { get; set; }
        public TimeSpan TimerInterval { get; set; }
    }
}
