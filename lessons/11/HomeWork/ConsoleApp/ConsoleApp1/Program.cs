using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reminderItem1 = new ReminderItem(DateTimeOffset.Parse("13:45"), "first reminder");
            var reminderItem2 = new ReminderItem(DateTimeOffset.Parse("12:45"), "second reminder");

            reminderItem1.WriteProperties();
            reminderItem2.WriteProperties();
        }
    }
}
