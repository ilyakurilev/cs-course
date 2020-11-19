namespace Reminder.Tests
{
    using Reminder.Storage;
    using Reminder.Storage.Memory;

    public class AsyncReminderStorageBuilder
    {
        private ReminderItem[] _items = new ReminderItem[0];

        public AsyncReminderStorageBuilder WithItems(params ReminderItem[] items)
        {
            _items = items;
            return this;
        }       

        public AsyncReminderStorage Build()
        {
            return new AsyncReminderStorage(_items);
        }
    }
}
