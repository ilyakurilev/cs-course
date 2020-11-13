using System;

namespace Reminder.Storage
{
    public interface IReminderStorage
    {
        void Add(ReminderItem item);
        void Update(ReminderItem item);
        /// <summary>
        /// Returns item with matching by id
        /// </summary>
        /// <param name="id">The reminder id</param>
        /// <exception cref="ReminderItemNotFoundException">Raises if item with secified id is not found</exception>
        /// <returns>
        /// The reminder <see cref="ReminderItem"/>
        /// </returns>
        ReminderItem Get(Guid id);
        ReminderItem[] Find(DateTimeOffset dateTime, ReminderItemStatus status = ReminderItemStatus.Created);
    }
}
