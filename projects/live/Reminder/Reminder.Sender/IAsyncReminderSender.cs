using System.Threading.Tasks;

namespace Reminder.Sender
{
    public interface IAsyncReminderSender
    {
        Task SendAsync(ReminderNotification notification);
    }
}
