using System;

namespace Reminder
{
    using Reminder.Domain;
    using Reminder.Storage.Memory;
    using Reminder.Sender.Telegram;
    using Reminder.Receiver.Telegram;
    using System.Threading;

    class Program
    {
        private const string Token = "";

        static void Main(string[] args)
        {
            var scheduler = new ReminderScheduler(
                new ReminderStorage(),
                new ReminderSender(Token),
                new ReminderReceiver(Token)
            );
 
            scheduler.ReminderSent += OnReminderSent;

            var cancellationToken = new CancellationTokenSource().Token;
            
            var t = scheduler.StartAsync(new ReminderSchedulerSettings
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
