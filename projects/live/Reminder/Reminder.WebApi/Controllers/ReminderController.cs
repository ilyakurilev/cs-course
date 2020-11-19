using Microsoft.AspNetCore.Mvc;
using Reminder.Storage;
using Reminder.WebApi.ViewModels;
using System;
using System.Threading.Tasks;

namespace Reminder.WebApi.Controllers
{
    [Route("/api/reminders")]
    public class ReminderController : Controller
    {
        private readonly IAsyncReminderStorage _storage;

        public ReminderController(IAsyncReminderStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var item = await _storage.GetAsync(id);

            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> FindAsync([FromQuery] FindReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = await _storage.FindAsync(model.DateTime, model.Status);

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = new ReminderItem(
                model.Id ?? Guid.NewGuid(),
                model.Status,
                model.DateTime,
                model.Message,
                model.ContactId);

            await _storage.AddAsync(item);

            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var item = await _storage.GetAsync(id);
            var updatedItem = new ReminderItem(
                item.Id,
                model.Status,
                item.DateTime,
                model.Message,
                item.ContactId);

            await _storage.UpdateAsync(updatedItem);

            return Ok(updatedItem);
        }
    }
}
