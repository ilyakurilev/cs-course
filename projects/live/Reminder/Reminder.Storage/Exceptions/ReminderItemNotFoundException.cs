using System;

namespace Reminder.Storage.Exceptions
{
    public class ReminderItemNotFoundException : Exception
    {
        public Guid Id { get; }

        public ReminderItemNotFoundException(Guid id) :
            base($"Reminder item with id {id:N} not found")
        {
            Id = id;
        }
    }
}
