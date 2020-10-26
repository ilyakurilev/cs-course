using NUnit.Framework;
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

        [Test]
        public void GivenReminderWithPastDate_ShouldRaiseRaised()
        {
            var raised = false;
            using var scheduler = new ReminderScheduler(
                Create.Storage
                    .WithItems(Create.Reminder.AtUtcNow())
                    .Build(),
                new ReminderSender(fail: false)
            );
            scheduler.ReminderSent += (sender, args) => raised = true;

            scheduler.Start(DefaultSettings);
            WaitTimers();

            Assert.IsTrue(raised);
        }

        [Test]
		public void SenderThrowException_ShouldRaiseRaised()
		{
			var raised = false;
			using var scheduler = new ReminderScheduler(
				Create.Storage
					.WithItems(Create.Reminder.AtUtcNow())
					.Build(),
				new ReminderSender(fail: true)
			);
			scheduler.ReminderFailed += (sender, args) => raised = true;

			scheduler.Start(DefaultSettings);
			WaitTimers();

			Assert.IsTrue(raised);
		}


		private void WaitTimers()
		{
			Thread.Sleep(DefaultSettings.TimerInterval.Milliseconds * 2);
		}

	}
}