using System;

namespace ConsoleApp1
{
    partial class ReminderItem
    {
        public DateTimeOffset AlarmDate { get; set; }
        public string AlarmMessage { get; set; }

        public TimeSpan TimeToAlarm =>
            DateTimeOffset.Now - AlarmDate;

        public bool IsOutdated =>
            TimeToAlarm.TotalMilliseconds >= 0;

        public ReminderItem(DateTimeOffset alarmDate, string alarmMessage)
        {
            AlarmDate = alarmDate;
            AlarmMessage = alarmMessage;
        }
    }
}
