using NUnit.Framework;
using Reminder.Storage.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

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
                storage.Get(Guid.NewGuid()));

            Assert.AreEqual(itemId, exception.Id);
        }

        public void Get_GivenExistingItem_ShouldReturnIt()
        {
            var storage = new ReminderStorage();
            var itemId = Guid.NewGuid();

            var item = storage.Get(itemId);

            Assert.AreEqual(itemId, item.Id);
        }
    }
}
