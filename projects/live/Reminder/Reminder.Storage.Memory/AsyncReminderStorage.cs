using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Reminder.Storage.Memory
{
    using Reminder.Storage.Memory.Extensions;

    public class AsyncReminderStorage : ReminderStorage, IAsyncReminderStorage
    {
        private readonly ConcurrentDictionary<Guid, ReminderItem> _items;

        public AsyncReminderStorage()
        {
            _items = new ConcurrentDictionary<Guid, ReminderItem>();
        }

        public AsyncReminderStorage(params ReminderItem[] items)
        {
            _items = items.ToConcurrentDictionary(item => item.Id);
        }

        public async Task AddAsync(ReminderItem item)
        {
            await Task.Run(() => Add(item));
        }

        public async Task<ReminderItem[]> FindAsync(DateTimeOffset dateTime, ReminderItemStatus status = ReminderItemStatus.Created)
        {
            return await Task.Run(() => Find(dateTime, status));
        }

        public async Task<ReminderItem> GetAsync(Guid id)
        {
            return await Task.Run(() => Get(id));
        }

        public async Task UpdateAsync(ReminderItem item)
        {
            await Task.Run(() => Update(item));
        }
    }
}
