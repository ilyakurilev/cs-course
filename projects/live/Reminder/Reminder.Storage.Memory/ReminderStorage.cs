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
<<<<<<< HEAD
                throw new NotImplementedException();
=======
                throw new ReminderItemAlreadyExistException(item.Id);
>>>>>>> 53f77d99d35bb4b87da0099a1039d0cc66a88d05
            }
        }

        public ReminderItem[] Find(DateTimeOffset dateTime)
        {
            return _items.Values.
                Where(item => item.DateTime <= dateTime && item.Status == ReminderItemStatus.Created).
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
<<<<<<< HEAD
                throw new NotImplementedException();
=======
                throw new ReminderItemNotFoundException(item.Id);
>>>>>>> 53f77d99d35bb4b87da0099a1039d0cc66a88d05
            }

            _items[item.Id] = item;
        }
    }
}
