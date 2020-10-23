using Reminder.Storage;
using System;
using System.Threading;

namespace Reminder.Domain
{
    public class ReminderScheduler : IDisposable
    {
        public event EventHandler<ReminderSentEventArgs> ReminderSent;

        private readonly IReminderStorage _storage;
        private Timer _timer;

        private bool IsDisposed =>
            _timer == null;

        public ReminderScheduler(IReminderStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public void Start(ReminderSchedulerSettings settings)
        {
            _timer = new Timer(OnTimerTick, null, settings.TimerDelay, settings.TimerInterval);
        }

        private void OnTimerTick(object state)
        {
            var dateTime = DateTimeOffset.UtcNow;
            var reminders = _storage.Find(dateTime);

            foreach (var reminder in reminders)
            {
                reminder.MakeReady();
                ReminderSent?.Invoke(this, new ReminderSentEventArgs(reminder));
                reminder.MakeSent();
            }
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
