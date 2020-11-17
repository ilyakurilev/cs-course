using Reminder.Storage;
using System;
using System.Threading;
using Reminder.Sender;
using Reminder.Sender.Exceptions;
using Reminder.Receiver;

namespace Reminder.Domain
{

    public class ReminderScheduler : IDisposable
    {
        public event EventHandler<ReminderEventArgs> ReminderSent;
        public event EventHandler<ReminderEventArgs> ReminderFailed;

        private readonly IReminderStorage _storage;
        private readonly IReminderSender _sender;
        private readonly IReminderReceiver _receiver;
        private Timer _timer;

        private bool IsDisposed =>
            _timer == null;

        public ReminderScheduler(IReminderStorage storage, IReminderSender sender, IReminderReceiver receiver)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
        }

        public void Start(ReminderSchedulerSettings settings)
        {
            _timer = new Timer(OnTimerTick, null, settings.TimerDelay, settings.TimerInterval);
            _receiver.MessageReceived += OnMessageReceived;
        }

        private void OnTimerTick(object state)
        {
            var dateTime = DateTimeOffset.UtcNow;
            var reminders = _storage.Find(dateTime);

            foreach (var reminder in reminders)
            {
                reminder.MarkReady();
                try
                {
                    _sender.Send(new ReminderNotification(
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
            _storage.Add(item);
        }

        public void OnReminderSent(ReminderItem reminder)
        {
            reminder.MarkSent();
            ReminderSent?.Invoke(this, new ReminderEventArgs(reminder));
        }

        public void OnReminderFailed(ReminderItem reminder)
        {
            reminder.MarkFailed();
            ReminderFailed?.Invoke(this, new ReminderEventArgs(reminder));
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            _timer.Dispose();
            _timer = null;
        }
    }
}
