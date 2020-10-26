

namespace Reminder.Tests
{
    public static class Create
    {
        public static ReminderStorageBuilder Storage =>
            new ReminderStorageBuilder();

        public static ReminderItemBuilder Reminder =>
            new ReminderItemBuilder();
    }
}
