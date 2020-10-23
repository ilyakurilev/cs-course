using NuGet.Frameworks;
using NUnit.Framework;
using Reminder.Storage.Exceptions;
using System;

namespace Reminder.Storage.Memory.Tests
{
    class ReminderStorageTests
    {
        [Test]
        public void Get_GivenNotExistingId_ShouldRaiseException()
        {
            var storage = new ReminderStorage();
            var itemId = Guid.NewGuid();

            var exception = Assert.Catch<ReminderItemNotFoundException>(() =>
                storage.Get(itemId));

            Assert.AreEqual(itemId, exception.Id);
        }

        [Test]
        public void Get_GivenExistingItem_ShouldReturnIt()
        {
            var item = ReminderItem(Guid.NewGuid());
            var storage = new ReminderStorage(item);

            var result = storage.Get(item.Id);

            Assert.AreEqual(item.Id, result.Id);
        }

        [Test]
        public void Add_GivenNotExistingItem_ShouldAddIt()
        {
            var storage = new ReminderStorage();
            var item = ReminderItem(Guid.NewGuid());

            storage.Add(item);

            var addedItem = storage.Get(item.Id);

            Assert.AreEqual(item.Id, addedItem.Id);
        }

        [Test]
        public void Add_GivenExistingItem_ShouldRaiseException()
        {
            var itemId = Guid.NewGuid();
            var item = ReminderItem(itemId);
            var storage = new ReminderStorage(item);

            Assert.Catch<ReminderItemAlreadyExistException>(() =>
                storage.Add(item));
        }

        [Test]
        public void Update_GivenExistingItem_ShoutdUpdateIt()
        {
            var itemId = Guid.NewGuid();
            var item = ReminderItem(itemId);
            var storage = new ReminderStorage(item);

            var updatedItem = ReminderItem(itemId, ReminderItemStatus.Sent, null, "Updated message", "ContactId");
            storage.Update(updatedItem);

            var result = storage.Get(itemId);

            Assert.AreEqual(item.Id, result.Id);
            Assert.AreNotEqual(item.Status, result.Status);
            Assert.AreNotEqual(item.DateTime, result.DateTime);
            Assert.AreNotEqual(item.Message, result.Message);
        }
        
        [Test]
        public void Update_GivenNotExistingItem_ShouldRaiseException()
        {
            var itemId = Guid.NewGuid();
            var item = ReminderItem(itemId);
            var storage = new ReminderStorage();
            

            Assert.Catch<ReminderItemNotFoundException>(() =>
                storage.Update(item));
        }

        [Test]
        public void Find_GivenRemindersInFuture_ShouldReturnEmptyCollecton()
        {
            var items = new ReminderItem[]
            {
                ReminderItem(Guid.NewGuid(), status: ReminderItemStatus.Created, dateTime: DateTimeOffset.UtcNow.AddMinutes(10)),
                ReminderItem(Guid.NewGuid(), status: ReminderItemStatus.Created, dateTime: DateTimeOffset.UtcNow.AddMinutes(10)),
                ReminderItem(Guid.NewGuid(), status: ReminderItemStatus.Created, dateTime: DateTimeOffset.UtcNow.AddMinutes(10)),
            };
            var storage = new ReminderStorage();

            var result = storage.Find(DateTimeOffset.UtcNow);

            Assert.IsEmpty(result);
        }

        [Test]
        public void Find_GivenRemindersInPast_ShouldReturnNotEmptyCollection()
        {
            var items = new ReminderItem[]
            {
                ReminderItem(Guid.NewGuid(), status: ReminderItemStatus.Created),
                ReminderItem(Guid.NewGuid(), status: ReminderItemStatus.Created),
                ReminderItem(Guid.NewGuid(), status: ReminderItemStatus.Created),
            };
            var storage = new ReminderStorage(items);

            var result = storage.Find(DateTimeOffset.UtcNow);

            Assert.IsNotEmpty(result);
        }
        

        public ReminderItem ReminderItem(Guid id,
            ReminderItemStatus status = ReminderItemStatus.Created,
            DateTimeOffset? dateTime = default,
            string message = "Message",
            string contactId = "ContactId")
        {
            return new ReminderItem(id,
                status, 
                dateTime ?? DateTimeOffset.UtcNow, 
                message,
                contactId);
        }

    }
}
