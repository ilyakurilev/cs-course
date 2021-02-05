using Reminder.Sender;
using Reminder.Sender.Exceptions;
using System;
using System.Threading.Tasks;

namespace Reminder.Domain.Tests
{
    public class ReminderSender : IReminderSender
	{
		private readonly bool _fail;

		public ReminderSender(bool fail)
        {
			_fail = fail;
        }

		public Task SendAsync(ReminderNotification item)
		{
			if (_fail)
			{
				throw new ReminderSenderException(null);
			}
			return Task.CompletedTask;
		}
	}
}