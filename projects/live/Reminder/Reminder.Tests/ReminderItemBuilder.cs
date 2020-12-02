using Reminder.Storage;
using System;

namespace Reminder.Tests
{
    public class ReminderItemBuilder
    {
        private Guid _id = Guid.NewGuid();
        private ReminderItemStatus _status = ReminderItemStatus.Created;
        private DateTimeOffset _dateTime = DateTimeOffset.UtcNow;
        private string _message = "Message";
        private string _contactId = "ContactId";

        public ReminderItemBuilder AtUtcNow()
        {
            _dateTime = DateTimeOffset.UtcNow;
            return this;
        }

        public ReminderItemBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ReminderItemBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public ReminderItemBuilder WithContact(string contactId)
        {
            _contactId = contactId;
            return this;
        }

        public ReminderItemBuilder WithStatus(ReminderItemStatus status)
        {
            _status = status;
            return this;
        }

        public ReminderItemBuilder InFuture()
        {
            _dateTime = DateTimeOffset.UtcNow.AddMinutes(10);
            return this;
        }

        public ReminderItemBuilder InPast()
        {
            _dateTime = DateTimeOffset.UtcNow.AddMinutes(-10);
            return this;
        }

        public ReminderItem Build()
        {
            return new ReminderItem(
               _id,
               _status,
               _dateTime,
               _message,
               _contactId
               );
        }

        public static implicit operator ReminderItem(ReminderItemBuilder builder)
        {
            return new ReminderItem(
                builder._id,
                builder._status,
                builder._dateTime,
                builder._message,
                builder._contactId
                );
        }
    }
}