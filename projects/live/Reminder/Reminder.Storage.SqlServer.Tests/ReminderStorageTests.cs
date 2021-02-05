using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Reminder.Storage.SqlServer.Tests
{
    using Exceptions;
    using Reminder.Tests;

    public class ReminderStorageTests
    {
        [OneTimeSetUp]
        public async Task OneTimeSetUp() =>
            await DatabaseTestsHelper.ExecuteSqlQueryAsync(
                ReminderStorageSctipts.CreateTableReminderStatuses,
                ReminderStorageSctipts.CreateTableReminderContacts,
                ReminderStorageSctipts.CreateTableReminderItems,
                ReminderStorageSctipts.CreateProcedureAddReminderItem,
                ReminderStorageSctipts.CreateProcedureUpdateReminderItem);

        [SetUp]
        public async Task Setup() =>
            await DatabaseTestsHelper.ExecuteSqlQueryAsync(
                ReminderStorageSctipts.InsertReminderStatuses);

        [TearDown]
        public async Task TearDown() =>
            await DatabaseTestsHelper.ExecuteSqlQueryAsync(
                "DELETE FROM [ReminderItems];",
                "DELETE FROM [ReminderContacts];",
                "DELETE FROM [ReminderStatuses];");

        [OneTimeTearDown]
        public async Task OneTimeTearDown() =>
            await DatabaseTestsHelper.ExecuteSqlQueryAsync(
                "DROP TABLE [ReminderItems]",
                "DROP TABLE [ReminderContacts];",
                "DROP TABLE [ReminderStatuses];",
                "DROP PROCEDURE [AddReminderItem];",
                "DROP PROCEDURE [UpdateReminderItem];");

        [Test]
        public void GetAsync_GivenIdOfNonexistentItem_ShouldRaiseException()
        {
            //Arrange
            var storage = new ReminderStorage(DatabaseTestsHelper.ConnectionString);
            var id = Guid.NewGuid();

            //Act
            var exception = Assert.CatchAsync<ReminderItemNotFoundException>(() => storage.GetAsync(id));

            //Assert
            Assert.AreEqual(id, exception.Id);
        }

        [Test]
        public async Task GetAsync_GivenIdOfExistingItem_ShouldReturnItemWithThisId()
        {
            //Arrange
            var storage = new ReminderStorage(DatabaseTestsHelper.ConnectionString);
            var id = Guid.NewGuid();
            var item = Create.Reminder
                .WithId(id)
                .Build();
            await storage.AddAsync(item);

            //Act
            var foundItem = await storage.GetAsync(id);

            //Assert
            Assert.AreEqual(item.Id, foundItem.Id);
            Assert.AreEqual(item.Status, foundItem.Status);
            Assert.AreEqual(item.DateTime, foundItem.DateTime);
            Assert.AreEqual(item.Message, foundItem.Message);
            Assert.AreEqual(item.ChatId, foundItem.ChatId);
        }

        [Test]
        public async Task AddAsync_GivenNonexistenItem_ShouldAddIt()
        {
            //Arrange
            var storage = new ReminderStorage(DatabaseTestsHelper.ConnectionString);
            var id = Guid.NewGuid();
            var item = Create.Reminder
                .WithId(id)
                .Build();

            //Act
            await storage.AddAsync(item);
            var found = await storage.GetAsync(id);

            //Assert
            Assert.AreEqual(item.Id, found.Id);
            Assert.AreEqual(item.Status, found.Status);
            Assert.AreEqual(item.DateTime, found.DateTime);
            Assert.AreEqual(item.Message, found.Message);
            Assert.AreEqual(item.ChatId, found.ChatId);
        }

        [Test]
        public async Task AddAsync_GivenExistingItem_ShoudRaiseException()
        {
            //Arrange
            var storage = new ReminderStorage(DatabaseTestsHelper.ConnectionString);
            var item = Create.Reminder
                .Build();

            //Act
            await storage.AddAsync(item);
            var exception = Assert.CatchAsync<ReminderItemAlreadyExistsException>(() => storage.AddAsync(item));

            //Assert
            Assert.AreEqual(item.Id, exception.Id);
        }

        [Test]
        public void UpdateAsync_GivenNonexistentItem_ShouldRaiseException()
        {
            //Arrange
            var storage = new ReminderStorage(DatabaseTestsHelper.ConnectionString);
            var id = Guid.NewGuid();
            var item = Create.Reminder
                .WithId(id)
                .Build();

            //Act
            var exception = Assert.CatchAsync<ReminderItemNotFoundException>(() => storage.UpdateAsync(item));

            //Assert
            Assert.AreEqual(item.Id, exception.Id);
        }

        [Test]
        public async Task UpdateAsync_GivenExistingItem_ShouldUpdateIt()
        {
            //Arrange
            var storage = new ReminderStorage(DatabaseTestsHelper.ConnectionString);
            var id = Guid.NewGuid();
            var notUpdatedMessage = "Not updated message";
            var notUpdatedStatus = ReminderItemStatus.Created;
            var item = Create.Reminder
                .WithId(id)
                .WithMessage(notUpdatedMessage)
                .WithStatus(notUpdatedStatus)
                .Build();

            await storage.AddAsync(item);

            var updatedMessage = "Updated message";
            var updatedStatus = ReminderItemStatus.Sent;
            var updatedItem = Create.Reminder
                .WithId(id)
                .WithMessage(updatedMessage)
                .WithStatus(updatedStatus)
                .Build();

            //Act
            await storage.UpdateAsync(updatedItem);
            var found = await storage.GetAsync(id);

            //Assert
            Assert.AreEqual(item.Id, found.Id);
            Assert.AreNotEqual(item.Message, found.Message);
            Assert.AreNotEqual(item.Status, found.Status);
            Assert.AreEqual(updatedMessage, found.Message);
            Assert.AreEqual(updatedStatus, found.Status);
        }

        [Test]
        public async Task FindAsync_GivenExistingItemsWithSpecifiedStatuses_ShouldReturnNotEmptyCollection()
        {
            //Arrange
            var storage = new ReminderStorage(DatabaseTestsHelper.ConnectionString);
            await storage.AddAsync(Create.Reminder.WithStatus(ReminderItemStatus.Created));
            await storage.AddAsync(Create.Reminder.WithStatus(ReminderItemStatus.Ready));
            await storage.AddAsync(Create.Reminder.WithStatus(ReminderItemStatus.Ready));

            //Act
            var found = await storage.FindAsync(ReminderItemFilter.ByStatus(ReminderItemStatus.Ready));

            //Assert
            CollectionAssert.IsNotEmpty(found);
        }

        [Test]
        public async Task FindAsync_GivenOneExistingItemInPast_ShouldReturnNotEmptyCollectionWithIt()
        {
            //Arrange
            var storage = new ReminderStorage(DatabaseTestsHelper.ConnectionString);
            await storage.AddAsync(Create.Reminder.InFuture());
            await storage.AddAsync(Create.Reminder.InPast());

            //Act
            var found = await storage.FindAsync(ReminderItemFilter.CreatedAtNow());

            //Assert
            CollectionAssert.IsNotEmpty(found);
        }

        [Test]
        public async Task FindAsync_GivenNonexistentItems_ShouldReturnEmptyCollection()
        {
            //Arrange
            var storage = new ReminderStorage(DatabaseTestsHelper.ConnectionString);
            await storage.AddAsync(Create.Reminder.InFuture());
            await storage.AddAsync(Create.Reminder.InFuture());

            //Act
            var found = await storage.FindAsync(ReminderItemFilter.CreatedAtNow());

            //Assert
            CollectionAssert.IsEmpty(found);
        }
    }
}