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
        private readonly IReminderStorage _storage;

        public ReminderController(IReminderStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _storage.GetAsync(id);
            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] FindReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var items = await _storage.FindAsync(model.DateTime, model.Status);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReminderViewModel model)
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
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReminderViewModel model)
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
