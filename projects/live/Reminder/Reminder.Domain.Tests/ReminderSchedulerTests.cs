using NUnit.Framework;
using Reminder.Receiver;
using Reminder.Storage;
using Reminder.Tests;
using System;
using System.Threading;

namespace Reminder.Domain.Tests
{

    public class ReminderSchedulerTests
    {
		public ReminderSchedulerSettings DefaultSettings =>
			new ReminderSchedulerSettings
			{
				TimerDelay = TimeSpan.Zero,
				TimerInterval = TimeSpan.FromMilliseconds(20)
			};

		public ReminderSender SuccessSender =>
			new ReminderSender(fail: false);

		public ReminderSender FailSender =>
			new ReminderSender(fail: true);

		public ReminderReceiver Receiver { get; } =
			new ReminderReceiver();

		public IReminderStorage Storage =>
			Create.Storage.Build();

		[Test]
        public void GivenReminderWithPastDate_ShouldRaiseRaised()
        {
            var raised = false;
            using var scheduler = new ReminderScheduler(Storage, SuccessSender, Receiver);
            scheduler.ReminderSent += (sender, args) => raised = true;

            scheduler.Start(DefaultSettings);
			Receiver.SendMessage(DateTimeOffset.UtcNow, "Message", "ContactId");
            WaitTimers();

            Assert.IsTrue(raised);
        }

        [Test]
		public void SenderThrowException_ShouldRaiseRaised()
		{
			var raised = false;
			using var scheduler = new ReminderScheduler(Storage, FailSender, Receiver);
			scheduler.ReminderFailed += (sender, args) => raised = true;

			scheduler.Start(DefaultSettings);
			Receiver.SendMessage(DateTimeOffset.UtcNow, "Message", "ContactId");
			WaitTimers();

			Assert.IsTrue(raised);
		}


		private void WaitTimers()
		{
			Thread.Sleep(DefaultSettings.TimerInterval.Milliseconds * 2);
		}

	}
}