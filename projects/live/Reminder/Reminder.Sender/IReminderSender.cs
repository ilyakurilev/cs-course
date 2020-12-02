using System.Threading.Tasks;

namespace Reminder.Sender
{
    public interface IReminderSender
    {
        Task SendAsync(ReminderNotification notification);
    }
}
