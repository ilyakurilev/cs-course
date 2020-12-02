using System;
using System.Threading.Tasks;

namespace Reminder.Domain
{
    using Reminder.Storage;
    using Reminder.Sender;
    using Reminder.Receiver;
    using Reminder.Sender.Exceptions;

    public class ReminderScheduler
    {
        public event EventHandler<ReminderEventArgs> ReminderSent;
        public event EventHandler<ReminderEventArgs> ReminderFailed;

        private readonly IReminderStorage _storage;
        private readonly IReminderSender _sender;
        private readonly IReminderReceiver _receiver;

        public ReminderScheduler(IReminderStorage storage, IReminderSender sender, IReminderReceiver receiver)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
        }

        public async Task StartAsync(ReminderSchedulerSettings settings)
        {
            _receiver.MessageReceived += OnMessageReceived;

            await Task.Delay(settings.TimerDelay);

            while (true)
            {
                await Task.Delay(settings.TimerInterval);
                await SendScheduledRemindersAsync();
            }
        }

        private async Task SendScheduledRemindersAsync()
        {
            var dateTime = DateTimeOffset.UtcNow;
            var reminders = await _storage.FindAsync(dateTime);

            foreach (var reminder in reminders)
            {
                reminder.MarkReady();
                try
                {
                    await _sender.SendAsync(new ReminderNotification(
                        reminder.DateTime,
                        reminder.Message,
                        reminder.ContactId
                        )
                    );
                    OnReminderSent(reminder);
                }
                catch (ReminderSenderException)
                {
                    OnReminderFailed(reminder);
                }
            }
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            var item = new ReminderItem(
                Guid.NewGuid(),
                ReminderItemStatus.Created,
                args.Message.DateTime,
                args.Message.Text,
                args.ContactId
            );
            _storage.AddAsync(item);
        }

        private void OnReminderSent(ReminderItem reminder)
        {
            reminder.MarkSent();
            ReminderSent?.Invoke(this, new ReminderEventArgs(reminder));
        }

        private void OnReminderFailed(ReminderItem reminder)
        {
            reminder.MarkFailed();
            ReminderFailed?.Invoke(this, new ReminderEventArgs(reminder));
        }
    }
}
