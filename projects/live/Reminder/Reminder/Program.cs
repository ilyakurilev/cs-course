using System;

namespace Reminder
{
    using Reminder.Domain;
    using Reminder.Storage.Memory;
    using Reminder.Sender.Telegram;
    using Reminder.Receiver.Telegram;

    class Program
    {
        private const string Token = "";

        static void Main(string[] args)
        {
            var scheduler = new AsyncReminderScheduler(
                new AsyncReminderStorage(),
                new AsyncReminderSender(Token),
                new ReminderReceiver(Token)
            );

            scheduler.ReminderSent += OnReminderSent;
            scheduler.Start(new ReminderSchedulerSettings
            {
                TimerDelay = TimeSpan.Zero,
                TimerInterval = TimeSpan.FromSeconds(1)
            });


            Console.WriteLine("Waiting reminders..");
            Console.WriteLine("Press any key to stop");
            Console.ReadKey(true);
        }

        public static void OnReminderSent(object sender, ReminderEventArgs args)
        {
            Console.WriteLine(
                $"Reminder ({args.Reminder.Id}) at " +
                $"{args.Reminder.DateTime:F} sent received with " +
                $"message {args.Reminder.Message}"
            );
        }
    }
}
