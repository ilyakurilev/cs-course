using System;
using System.Threading.Tasks;

namespace Reminder.Storage
{
    public interface IAsyncReminderStorage
    {
        Task AddAsync(ReminderItem item);
        Task UpdateAsync(ReminderItem item);
        Task<ReminderItem> GetAsync(Guid id);
        Task<ReminderItem[]> FindAsync(DateTimeOffset dateTime, ReminderItemStatus status = ReminderItemStatus.Created);
    }
}
