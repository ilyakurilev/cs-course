using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reminders = new ReminderItem[]
            {
                new ReminderItem(DateTimeOffset.Now.AddMinutes(10), "First reminder"),
                new PhoneReminderItem(DateTimeOffset.Now.AddHours(-10), "Second reminder", "8(999)999-99-99"),
                new ChatReminderItem(DateTimeOffset.Now.AddDays(1), "Third reminder", "New chat", "Admin")
            };

            foreach (var reminder in reminders)
            {
                Console.WriteLine("____");
                reminder.WriteProperties();
                Console.WriteLine("____");
            }
        }
    }
}
