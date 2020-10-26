using NuGet.Frameworks;
using NUnit.Framework;
using Reminder.Storage.Exceptions;
using Reminder.Tests;
using System;

namespace Reminder.Storage.Memory.Tests
{
    class ReminderStorageTests
    {
		[Test]
		public void Get_GivenNotExistingId_ShouldRaiseException()
		{
			var itemId = Guid.NewGuid();
			var storage = Create.Storage.Build();
			

			var exception = Assert.Catch<ReminderItemNotFoundException>(() =>
				storage.Get(itemId)
			);
			Assert.AreEqual(itemId, exception.Id);
		}


		[Test]
		public void Get_GivenExistingItem_ShouldReturnIt()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = Create.Reminder.WithId(itemId);
			var storage = Create.Storage.WithItems(item).Build();
			

			// Act
			var result = storage.Get(itemId);

			// Assert
			Assert.AreEqual(itemId, result.Id);
		}

		[Test]
		public void Add_GivenNotExistingId_ShouldGetByIdAfterAdd()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = Create.Reminder.WithId(itemId).Build();
			var storage = Create.Storage.Build();

			// Act
			storage.Add(item);
			var result = storage.Get(item.Id);

			// Assert
			Assert.AreEqual(item.Id, result.Id);
		}

		[Test]
		public void Add_GivenExistingItem_ShouldRaiseException()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = Create.Reminder.WithId(itemId).Build();
			var storage = Create.Storage.WithItems(item).Build();

			// Act
			var exception = Assert.Catch<ReminderItemAlreadyExistsException>(() =>
				storage.Add(item)
			);

			// Assert
			Assert.AreEqual(item.Id, exception.Id);
		}

		[Test]
		public void Update_GivenNotExistingId_ShouldRaiseException()
		{
			var itemId = Guid.NewGuid();
			var item = Create.Reminder.WithId(itemId).Build();
			var storage = Create.Storage.Build();
			

			var exception = Assert.Catch<ReminderItemNotFoundException>(() =>
				storage.Update(item)
			);
			Assert.AreEqual(item.Id, exception.Id);
		}

		[Test]
		public void Update_GivenExistingItem_ShouldReturnUpdatedValues()
		{
			var itemId = Guid.NewGuid();
			var item = Create.Reminder.
				WithId(itemId).
				WithMessage("Initial message").
				WithContact("Initial contact").
				Build();
			var storage = Create.Storage.WithItems(item).Build();
			var updatedItem = Create.Reminder.
				WithId(itemId).
				WithMessage("Updated message").
				WithContact("Updated contact").
				Build();


			storage.Update(updatedItem);
			var result = storage.Get(item.Id);


			Assert.AreEqual(updatedItem.Message, result.Message);
			Assert.AreEqual(updatedItem.ContactId, result.ContactId);
		}

		[Test]
		public void Find_GivenRemindersInFuture_ShouldReturnEmptyCollection()
		{
			var datetime = DateTimeOffset.UtcNow;
			var storage = Create.Storage.
				WithItems(
				Create.Reminder.InFuture(),
				Create.Reminder.InFuture()).
				Build();

			var result = storage.Find(datetime);

			CollectionAssert.IsEmpty(result);
		}

		[Test]
		public void Find_GivenRemindersInPastOrEqual_ShouldReturnNotEmptyCollection()
		{
			var datetime = DateTimeOffset.UtcNow;
			var storage = Create.Storage.
				WithItems(
				Create.Reminder.InPast(),
				Create.Reminder.InPast()).
				Build();

			var result = storage.Find(datetime);

			CollectionAssert.IsNotEmpty(result);
		}
	}
}
