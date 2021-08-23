using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Reminder.Storage.Exceptions;
using Reminder.Storage.SqlServer.Ef.Entities;

namespace Reminder.Storage.SqlServer.Ef
{
    public class ReminderStorage : IReminderStorage
    {
        private readonly ReminderStorageContext _context;

        public ReminderStorage(ReminderStorageContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ReminderItem item)
        {
            var contact = await _context.ReminderContacts.FirstOrDefaultAsync(_ => _.ChatId == item.ChatId) ??
                          new ReminderContactEntity(item.ChatId);
            
            _context.ReminderItems.Add(new ReminderItemEntity(item, contact));
            
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException exception) when (exception.InnerException is SqlException {Number: 2627})
            {
                throw new ReminderItemAlreadyExistsException(item.Id);
            }
        }

        public async Task<ReminderItem[]> FindAsync(ReminderItemFilter filter)
        {
            var items = _context.ReminderItems
                .Include(_ => _.Contact)
                .AsQueryable();
            
            if (filter.Status.HasValue)
            {
                items = items.Where(_ => _.StatusId == (int) filter.Status.Value);
            }

            if (filter.DateTime.HasValue)
            {
                items = items.Where(_ => _.DateTime <= filter.DateTime.Value);
            }

            return await items
                .OrderByDescending(_ => _.DateTime)
                .Select(_ => _.ToReminderItem())
                .ToArrayAsync();
        }

        public async Task<ReminderItem> GetAsync(Guid id)
        {
            var entity = await _context.ReminderItems
                             .Include(_ => _.Contact)
                             .FirstOrDefaultAsync(_ => _.Id == id) ??
                         throw new ReminderItemNotFoundException(id);

            return entity.ToReminderItem();
        }

        public async Task UpdateAsync(ReminderItem item)
        {
            var entity = await _context.ReminderItems.FindAsync(item.Id) ??
                         throw new ReminderItemNotFoundException(item.Id);

            entity.Message = item.Message;
            entity.StatusId = (int) item.Status;

            await _context.SaveChangesAsync();
        }
    }
}
