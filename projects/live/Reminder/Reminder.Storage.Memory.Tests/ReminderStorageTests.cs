using NUnit.Framework;
using Reminder.Storage.Exceptions;
using Reminder.Tests;
using System;
using System.Threading.Tasks;

namespace Reminder.Storage.Memory.Tests
{
    class ReminderStorageTests
    {
		[Test]
		public void Get_GivenNotExistingId_ShouldRaiseException()
		{
			var itemId = Guid.NewGuid();
			var storage = Create.Storage.Build();
			

			var exception = Assert.CatchAsync<ReminderItemNotFoundException>(() =>
				storage.GetAsync(itemId)
			);
			Assert.AreEqual(itemId, exception.Id);
		}


		[Test]
		public async Task Get_GivenExistingItem_ShouldReturnIt()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = Create.Reminder.WithId(itemId);
			var storage = Create.Storage.WithItems(item).Build();
			

			// Act
			var result = await storage.GetAsync(itemId);

			// Assert
			Assert.AreEqual(itemId, result.Id);
		}

		[Test]
		public async Task Add_GivenNotExistingId_ShouldGetByIdAfterAdd()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = Create.Reminder.WithId(itemId).Build();
			var storage = Create.Storage.Build();

			// Act
			await storage.AddAsync(item);
			var result = await storage.GetAsync(item.Id);

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
			var exception = Assert.CatchAsync<ReminderItemAlreadyExistsException>(() =>
				storage.AddAsync(item)
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
			

			var exception = Assert.CatchAsync<ReminderItemNotFoundException>(() =>
				storage.UpdateAsync(item)
			);
			Assert.AreEqual(item.Id, exception.Id);
		}

		[Test]
		public async Task Update_GivenExistingItem_ShouldReturnUpdatedValues()
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


			await storage.UpdateAsync(updatedItem);
			var result = await storage.GetAsync(item.Id);


			Assert.AreEqual(updatedItem.Message, result.Message);
			Assert.AreEqual(updatedItem.ChatId, result.ChatId);
		}

		[Test]
		public async Task Find_GivenRemindersInFuture_ShouldReturnEmptyCollection()
		{
			var datetime = DateTimeOffset.UtcNow;
			var storage = Create.Storage.
				WithItems(
				Create.Reminder.InFuture(),
				Create.Reminder.InFuture()).
				Build();


			var result = await storage.FindAsync(ReminderItemFilter.CreatedAt(datetime));

			CollectionAssert.IsEmpty(result);
		}

		[Test]
		public async Task Find_GivenRemindersInPastOrEqual_ShouldReturnNotEmptyCollection()
		{
			var datetime = DateTimeOffset.UtcNow;
			var storage = Create.Storage.
				WithItems(
				Create.Reminder.InPast(),
				Create.Reminder.InPast()).
				Build();

			var result = await storage.FindAsync(ReminderItemFilter.CreatedAt(datetime));

			CollectionAssert.IsNotEmpty(result);
		}
	}
}
