using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Reminder.Storage.Memory
{
    using Exceptions;
    using Reminder.Storage.Memory.Extensions;

    public class ReminderStorage : IReminderStorage
    {
        private readonly ConcurrentDictionary<Guid, ReminderItem> _items;

        public ReminderStorage()
        {
            _items = new ConcurrentDictionary<Guid, ReminderItem>();
        }

        public ReminderStorage(params ReminderItem[] items)
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

        public Task<ReminderItem[]> FindAsync(ReminderItemFilter filter)
        {
            var result = _items.Values.AsEnumerable();
            if (filter.DateTime.HasValue)
            {
                result = result.Where(item => item.DateTime <= filter.DateTime);
            }
            if (filter.Status.HasValue)
            {
                result = result.Where(item => item.Status == filter.Status);
            }
            result = result.OrderByDescending(item => item.DateTime);

            return Task.FromResult(result.ToArray());
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
