using System;

namespace ConsoleApp1
{
    partial class ReminderItem
    {
        public void WriteProperties()
        {
            Console.WriteLine("____");
            Console.WriteLine($"AlarmDate: {AlarmDate}");
            Console.WriteLine($"AlarmMessage: {AlarmMessage}");
            Console.WriteLine($"TimeToAlarm: {TimeToAlarm}");
            Console.WriteLine($"IsOutdated: {IsOutdated}");
            Console.WriteLine("____");
        }
    }
}
