using System;
using System.Linq;
using System.Collections.Generic;

namespace Reminder.Storage.Memory
{
    using Exceptions;

    public class ReminderStorage : IReminderStorage
    {
        private readonly Dictionary<Guid, ReminderItem> _items;

        public ReminderStorage()
        {
            _items = new Dictionary<Guid, ReminderItem>();
        }

        public ReminderStorage(params ReminderItem[] items)
        {
            _items = items.ToDictionary(item => item.Id);
        }

        public void Add(ReminderItem item)
        {
            if (!_items.TryAdd(item.Id, item))
            {
                throw new ReminderItemAlreadyExistsException(item.Id);
            }
        }

        public ReminderItem[] Find(DateTimeOffset dateTime, ReminderItemStatus status)
        {
            return _items.Values.
                Where(item => item.DateTime <= dateTime && item.Status == status).
                OrderByDescending(item => item.DateTime).
                ToArray();
        }

        public ReminderItem Get(Guid id)
        {
            if (!_items.TryGetValue(id, out var item))
            {
                throw new ReminderItemNotFoundException(id);
            }

            return item;   
        }

        public void Update(ReminderItem item)
        {
            if (!_items.ContainsKey(item.Id))
            {
                throw new ReminderItemNotFoundException(item.Id);
            }

            _items[item.Id] = item;
        }
    }
}
