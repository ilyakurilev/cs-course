using System;

namespace ConsoleApp1
{
    partial class ReminderItem
    {
        public virtual void WriteProperties()
        {
            Console.WriteLine($"Type of reminder: {this.GetType()}");
            Console.WriteLine($"AlarmDate: {AlarmDate}");
            Console.WriteLine($"AlarmMessage: {AlarmMessage}");
            Console.WriteLine($"TimeToAlarm: {TimeToAlarm}");
            Console.WriteLine($"IsOutdated: {IsOutdated}");
        }
    }
}
