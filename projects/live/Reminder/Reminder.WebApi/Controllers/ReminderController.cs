using Microsoft.AspNetCore.Mvc;
using Reminder.Storage;
using Reminder.Storage.Exceptions;
using Reminder.WebApi.ViewModels;
using System;

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
        public IActionResult Get(Guid id)
        {
            try
            {
                var item = _storage.Get(id);
                return Ok(item);
            }
            catch (ReminderItemNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Find([FromQuery] FindReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var items = _storage.Find(model.DateTime, model.Status);
            return Ok(items);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var item = new ReminderItem(
                model.Id ?? Guid.NewGuid(),
                model.Status,
                model.DateTime,
                model.Message,
                model.ContactId);

            try
            {
                _storage.Add(item);
                return CreatedAtAction("Get", new { id = item.Id }, item);
            }
            catch (ReminderItemAlreadyExistsException)
            {
                return Conflict();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdateReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var item = _storage.Get(id);
                var updatedItem = new ReminderItem(
                    item.Id,
                    model.Status,
                    item.DateTime,
                    model.Message,
                    item.ContactId);
                _storage.Update(updatedItem);
                return Ok(updatedItem);
            }
            catch (ReminderItemNotFoundException)
            {
                return BadRequest();
            }

        }
    }
}
