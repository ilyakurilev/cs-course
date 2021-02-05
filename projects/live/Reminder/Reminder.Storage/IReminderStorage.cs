using System;
using System.Threading.Tasks;

namespace Reminder.Storage
{
    public interface IReminderStorage
    {
        Task AddAsync(ReminderItem item);
        Task UpdateAsync(ReminderItem item);
        /// <summary>
        /// Returns item with matching by id
        /// </summary>
        /// <param name="id">The reminder id</param>
        /// <exception cref="ReminderItemNotFoundException">Raises if item with secified id is not found</exception>
        /// <returns>
        /// The reminder <see cref="ReminderItem"/>
        /// </returns>
        Task<ReminderItem> GetAsync(Guid id);
        Task<ReminderItem[]> FindAsync(ReminderItemFilter filter);
    }
}
