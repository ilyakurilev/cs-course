using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reminder.Domain
{
    using Storage;
    using Sender;
    using Receiver;
    using Sender.Exceptions;

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

        public async Task StartAsync(ReminderSchedulerSettings settings, CancellationToken cancellationToken)
        {
            _receiver.MessageReceived += OnMessageReceived;

            await Task.Delay(settings.TimerDelay, cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                await SendScheduledRemindersAsync();
                await Task.Delay(settings.TimerInterval, cancellationToken);
            }
        }

        private async Task SendScheduledRemindersAsync()
        {
            var reminders = await _storage.FindAsync(ReminderItemFilter.CreatedAtNow());

            foreach (var reminder in reminders)
            {
                reminder.MarkReady();
                await _storage.UpdateAsync(reminder);
                try
                {
                    await _sender.SendAsync(new ReminderNotification(
                        reminder.DateTime,
                        reminder.Message,
                        reminder.ChatId
                        )
                    );
                    await OnReminderSentAsync(reminder);
                }
                catch (ReminderSenderException)
                {
                    await OnReminderFailedAsync(reminder);
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

        private async Task OnReminderSentAsync(ReminderItem reminder)
        {
            reminder.MarkSent();
            await _storage.UpdateAsync(reminder);
            ReminderSent?.Invoke(this, new ReminderEventArgs(reminder));
        }

        private async Task OnReminderFailedAsync(ReminderItem reminder)
        {
            reminder.MarkFailed();
            await _storage.UpdateAsync(reminder);
            ReminderFailed?.Invoke(this, new ReminderEventArgs(reminder));
        }
    }
}
