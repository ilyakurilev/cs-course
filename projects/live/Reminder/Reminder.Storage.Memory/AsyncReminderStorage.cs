using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Reminder.Storage.Memory
{
    using Reminder.Storage.Exceptions;
    using Reminder.Storage.Memory.Extensions;

    public class AsyncReminderStorage : IAsyncReminderStorage
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

        public Task AddAsync(ReminderItem item)
        {
            if (!_items.TryAdd(item.Id, item))
            {
                throw new ReminderItemAlreadyExistsException(item.Id);
            }
            return Task.CompletedTask;
        }

        public Task<ReminderItem[]> FindAsync(DateTimeOffset dateTime, ReminderItemStatus status = ReminderItemStatus.Created)
        {
            var result = _items.Values.
                Where(item => item.DateTime <= dateTime && item.Status == status).
                OrderByDescending(item => item.DateTime).
                ToArray();

            return Task.FromResult(result);
        }

        public Task<ReminderItem> GetAsync(Guid id)
        {
            if (!_items.TryGetValue(id, out var item))
            {
                throw new ReminderItemNotFoundException(id);
            }

            return Task.FromResult(item);
        }

        public Task UpdateAsync(ReminderItem item)
        {
            if (!_items.ContainsKey(item.Id))
            {
                throw new ReminderItemNotFoundException(item.Id);
            }

            _items[item.Id] = item;

            return Task.CompletedTask;
        }
    }
}
