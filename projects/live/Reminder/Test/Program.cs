using Reminder.Storage;
using Reminder.Storage.Exceptions;
using Reminder.Storage.Memory;
using System;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var storage = new AsyncReminderStorage();
            var item = new ReminderItem(Guid.NewGuid(), ReminderItemStatus.Created, DateTimeOffset.UtcNow, "Message", "ContactId");

            try
            {
                storage.AddAsync(item);
                storage.AddAsync(item);
            }
            catch (ReminderItemAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
