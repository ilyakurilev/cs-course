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

        public void Add(ReminderItem item)
        {
            throw new NotImplementedException();
        }

        public ReminderItem[] Find(DateTimeOffset dateTime)
        {
            return _items.Values.
                Where(item => item.DateTime <= dateTime).
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
            throw new NotImplementedException();
        }
    }
}
