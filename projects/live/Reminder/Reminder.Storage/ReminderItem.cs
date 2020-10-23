using System;

namespace Reminder.Storage
{
    /// <summary>
    /// Represents one reminder
    /// </summary>
    public class ReminderItem
    {
        /// <summary>
        /// Unique identefier
        /// </summary>
        public Guid Id { get; }
        /// <summary>
        /// The status from <see cref="ReminderItemStatus"/>
        /// </summary>
        public ReminderItemStatus Status { get; private set; }
        /// <summary>
        /// Date and time to sent in UTC format
        /// </summary>
        public DateTimeOffset DateTime { get; private set; }
        public string Message { get; private set; }
        public string ContactId { get; private set; }

        public ReminderItem(Guid id,
            ReminderItemStatus status,
            DateTimeOffset dateTime,
            string message,
            string contactId)
        {
            Id = id;
            Status = status;
            DateTime = dateTime;
            Message = message;
            ContactId = contactId;
        }

        public void MakeSent()
        {
            if (Status != ReminderItemStatus.Ready)
            {
                throw new InvalidOperationException($"Reminder should be in {ReminderItemStatus.Ready} status");
            }

            Status = ReminderItemStatus.Sent;
        }

        public void MakeReady()
        {
            if (Status == ReminderItemStatus.Ready)
            {
                throw new InvalidOperationException($"Reminder already in {ReminderItemStatus.Ready} status");
            }

            Status = ReminderItemStatus.Ready;
        }

        public override string ToString() =>
            $"Reminder (id: {Id}, status: {Status}) at {DateTime:O} to {ContactId}";

    }
}
