using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Reminder.Storage.Memory.Tests
{
    using Reminder.Storage.Exceptions;
    using Reminder.Tests;

    class AsyncReminderStorageTests
    {
        [Test]
        public async Task AddAsync_GivenNotExsistingItem_ShoudGetByIdAfterAdd()
        {
            var id = Guid.NewGuid();
            var storage = Create.AsyncStorage.Build();
            var item = Create.Reminder.WithId(id);

            await storage.AddAsync(item);

            var addedItem =  await storage.GetAsync(id);

            Assert.AreEqual(id, addedItem.Id);
        }

        [Test]
        public async Task AddAsync_GivenExsitingItem_ShouldRaiseException()
        {
            var id = Guid.NewGuid();
            var storage = Create.AsyncStorage.Build();
            var item = Create.Reminder.WithId(id);

            await storage.AddAsync(item);

            Assert.ThrowsAsync<ReminderItemAlreadyExistsException>(async () => await storage.AddAsync(item));
        }
    }
}
