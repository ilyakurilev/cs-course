using System;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Reminder.Storage.WebApi.Tests
{
    using Reminder.Storage.Exceptions;
    using Reminder.Storage.WebApi;
    using Reminder.WebApi;
    using Reminder.Tests;
    using System.Threading.Tasks;

    class ReminderStorageTests
    {
        private WebApplicationFactory<Startup> Factory =>
            new WebApplicationFactory<Startup>();


        [Test]
        public void GetAsync_WhenReminderNotExists_ShouldRiseException()
        {
            var storage = new ReminderStorage(Factory.CreateClient());

            Assert.ThrowsAsync<ReminderItemNotFoundException>(
                () => storage.GetAsync(Guid.NewGuid())
            );
        }

        [Test]
        public async Task GetAsync_WhenReminderExists_ShouldReturnIt()
        {
            var id = Guid.NewGuid();
            var storage = new ReminderStorage(Factory.CreateClient());
            var item = Create.Reminder.WithId(id).Build();

            await storage.AddAsync(item);
            var gotItem = await storage.GetAsync(id);

            Assert.AreEqual(item.Id, gotItem.Id);
            Assert.AreEqual(item.Status, gotItem.Status);
            Assert.AreEqual(item.DateTime, gotItem.DateTime);
            Assert.AreEqual(item.Message, gotItem.Message);
            Assert.AreEqual(item.ChatId, gotItem.ChatId);
        }

        [Test]
        public async Task CreateAsync_WithCorrectData_ShouldReturnById()
        {
            var id = Guid.NewGuid();
            var storage = new ReminderStorage(Factory.CreateClient());

            await storage.AddAsync(Create.Reminder.WithId(id));
            var item = await storage.GetAsync(id);

            Assert.IsNotNull(item);
            Assert.AreEqual(id, item.Id);
        }

        [Test]
        public void CreateAsync_WithIncorrectData_ShouldRiseException()
        {
            var storage = new ReminderStorage(Factory.CreateClient());

            Assert.CatchAsync(
                () => storage.AddAsync(Create.Reminder.WithMessage(""))
            );
        }

        [Test]
        public async Task CreateAsync_AlreadyExistItem_ShouldRiseException()
        {
            var id = Guid.NewGuid();
            var storage = new ReminderStorage(Factory.CreateClient());
            var item = Create.Reminder.WithId(id);

            await storage.AddAsync(item);

            Assert.ThrowsAsync<ReminderItemAlreadyExistsException>(
                () => storage.AddAsync(item)
            );
        }

        [Test]
        public async Task UpdateAsync_WhenReminderExists_ShouldUpdateIt()
        {
            var id = Guid.NewGuid();
            var storage = new ReminderStorage(Factory.CreateClient());
            var item = Create.Reminder.WithId(id).Build();

            await storage.AddAsync(item);
            await storage.UpdateAsync(Create.Reminder.WithId(id).WithMessage("Updated message"));

            var updatedItem = await storage.GetAsync(id);

            Assert.AreEqual(item.Id, updatedItem.Id);
            Assert.AreNotEqual(item.Message, updatedItem.Message);
        }

        [Test]
        public void UpdateAsync_WhenReminderNotExists_ShouldRiseException()
        {
            var storage = new ReminderStorage(Factory.CreateClient());

            Assert.ThrowsAsync<ReminderItemNotFoundException>(
                () => storage.UpdateAsync(Create.Reminder)
            );
        }

        [Test]
        public void FindAsync_WithIncorrectDate_ShouldRiseException()
        {
            var storage = new ReminderStorage(Factory.CreateClient());

            Assert.CatchAsync(
                () => storage.FindAsync(ReminderItemFilter.CreatedAt(DateTimeOffset.MinValue))
            );
        }

        [Test]
        public async Task FindAsync_WithCorrectDate_ShouldReturnReminders()
        {
            var storage = new ReminderStorage(Factory.CreateClient());

            await storage.AddAsync(Create.Reminder.InPast());

            var foundReminders = await storage.FindAsync(ReminderItemFilter.CreatedAtNow());

            CollectionAssert.IsNotEmpty(foundReminders);
        }
    }
}
