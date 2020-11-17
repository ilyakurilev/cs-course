using System;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Reminder.Storage.WebApi.Tests
{
    using Reminder.Storage.Exceptions;
    using Reminder.Storage.WebApi;
    using Reminder.WebApi;
    using Reminder.Tests;

    class ReminderStorageTests
    {
        private WebApplicationFactory<Startup> Factory =>
            new WebApplicationFactory<Startup>();


        [Test]
        public void Get_WhenReminderNotExists_ShouldRiseException()
        {
            var storage = new ReminderStorage(Factory.CreateClient());

            Assert.Throws<ReminderItemNotFoundException>(
                () => storage.Get(Guid.NewGuid())
            );
        }

        [Test]
        public void Get_WhenReminderExists_ShouldReturnIt()
        {
            var id = Guid.NewGuid();
            var storage = new ReminderStorage(Factory.CreateClient());
            var item = Create.Reminder.WithId(id).Build();

            storage.Add(item);
            var gotItem = storage.Get(id);

            Assert.AreEqual(item.Id, gotItem.Id);
            Assert.AreEqual(item.Status, gotItem.Status);
            Assert.AreEqual(item.DateTime, gotItem.DateTime);
            Assert.AreEqual(item.Message, gotItem.Message);
            Assert.AreEqual(item.ContactId, gotItem.ContactId);
        }

        [Test]
        public void Create_WithCorrectData_ShouldReturnById()
        {
            var id = Guid.NewGuid();
            var storage = new ReminderStorage(Factory.CreateClient());

            storage.Add(Create.Reminder.WithId(id));
            var item = storage.Get(id);

            Assert.IsNotNull(item);
            Assert.AreEqual(id, item.Id);
        }

        [Test]
        public void Create_WithIncorrectData_ShouldRiseException()
        {
            var storage = new ReminderStorage(Factory.CreateClient());

            Assert.Catch(
                () => storage.Add(Create.Reminder.WithMessage(""))
            );
        }

        [Test]
        public void Create_AlreadyExistItem_ShouldRiseException()
        {
            var id = Guid.NewGuid();
            var storage = new ReminderStorage(Factory.CreateClient());
            var item = Create.Reminder.WithId(id);

            storage.Add(item);

            Assert.Throws<ReminderItemAlreadyExistsException>(
                () => storage.Add(item)
            );
        }

        [Test]
        public void Update_WhenReminderExists_ShouldUpdateIt()
        {
            var id = Guid.NewGuid();
            var storage = new ReminderStorage(Factory.CreateClient());
            var item = Create.Reminder.WithId(id).Build();

            storage.Add(item);
            storage.Update(Create.Reminder.WithId(id).WithMessage("Updated message"));

            var updatedItem = storage.Get(id);

            Assert.AreEqual(item.Id, updatedItem.Id);
            Assert.AreNotEqual(item.Message, updatedItem.Message);
        }

        [Test]
        public void Update_WhenReminderNotExists_ShouldRiseException()
        {
            var storage = new ReminderStorage(Factory.CreateClient());

            Assert.Throws<ReminderItemNotFoundException>(
                () => storage.Update(Create.Reminder)
            );
        }

        [Test]
        public void Find_WithIncorrectDate_ShouldRiseException()
        {
            var storage = new ReminderStorage(Factory.CreateClient());

            Assert.Catch(
                () => storage.Find(new DateTimeOffset())
            );
        }

        [Test]
        public void Find_WithCorrectDate_ShouldReturnReminders()
        {
            var storage = new ReminderStorage(Factory.CreateClient());

            storage.Add(Create.Reminder.InPast());

            var foundReminders = storage.Find(DateTime.UtcNow);

            CollectionAssert.IsNotEmpty(foundReminders);
        }
    }
}
