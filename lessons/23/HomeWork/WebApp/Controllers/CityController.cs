using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Extensions;
using WebApp.Models;
using WebApp.Storage;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
	[Route("/api/cities")]
    public class CityController : Controller
    {
		private readonly IStorage<City> _storage;
		private readonly ILogger<CityController> _logger;

		public CityController(IStorage<City> storage, ILogger<CityController> logger)
		{
			_storage = storage;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult List([FromQuery] int page = 1)
		{
			return Ok(new ListCityViewModel(_storage.List(), page, 6));
		}

		[HttpGet("{id}")]
		public IActionResult Get(Guid id)
		{
			var city = _storage.Get(id);
			
			if (city == null)
			{
				_logger.LogWarning("Requested city with not existing id {0}", id);
				return NotFound();
			}

			return Ok(city);
		}

		[HttpPost]
		public IActionResult Create([FromBody] CityCreateViewModel info)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var city = new City(
				Guid.NewGuid(),
				info.Title.Trim().Capitalize(),
				info.Description.Trim().Capitalize(),
				info.Population);

			var duplicate = _storage.List().FirstOrDefault(_ => _.Title == city.Title);
			if (duplicate != null)
			{
				ModelState.AddModelError("Title", "Duplicate value");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_storage.Add(city);

			return CreatedAtAction("Get", new { id = city.Id }, city);
		}

		[HttpPut("{id}")]
		public IActionResult Update(Guid id, [FromBody] CityUpdateViewModel info)
		{
			if (!ModelState.IsValid)
            {
				return BadRequest(ModelState);
            }

			var city = _storage.Get(id);
			if (city == null)
			{
				return NotFound();
			}

			var updatedCity = new City(
				city.Id,
				city.Title,
				info.Description.Trim().Capitalize(),
				info.Population);

			if (city.Title == updatedCity.Description)
            {
				ModelState.AddModelError("Title", "The value duplicated with field Description");
            }

			if (!ModelState.IsValid)
            {
				return BadRequest(ModelState);
            }

			_storage.Remove(city);
			_storage.Add(updatedCity);
			return Ok(updatedCity);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
			var city = _storage.Get(id);
			if (city == null)
			{
				return NotFound();
			}

			_storage.Remove(city);
			return NoContent();
		}
	}
}
