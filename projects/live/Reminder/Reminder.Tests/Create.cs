namespace Reminder.Tests
{
    public static class Create
    {
        public static ReminderStorageBuilder Storage =>
            new ReminderStorageBuilder();

        public static AsyncReminderStorageBuilder AsyncStorage =>
            new AsyncReminderStorageBuilder();

        public static ReminderItemBuilder Reminder =>
            new ReminderItemBuilder();
    }
}
