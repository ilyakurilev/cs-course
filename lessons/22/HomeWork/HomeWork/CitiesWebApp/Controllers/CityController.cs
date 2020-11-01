using CitiesWebApp.Model;
using CitiesWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CitiesWebApp.Controllers
{
    [Route("/api/cities")]
    public class CityController : Controller
    {
        public Storage Storage =>
            Storage.Instance;

        [HttpGet]
        public IActionResult List()
        {
            return Ok(Storage.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var city = Storage
                .Cities
                .FirstOrDefault(_ => _.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CityCreateViewModel info)
        {
            var city = new City(Guid.NewGuid(), info.Title, info.Description, info.Population);
            Storage.Cities.Add(city);

            return CreatedAtAction("Get", new { Id = city.Id }, city);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var city = Storage
                .Cities
                .FirstOrDefault(_ => _.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            Storage.Cities.Remove(city);

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] CityCreateViewModel info, Guid id)
        {
            var city = Storage
                .Cities
                .FirstOrDefault(_ => _.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            city.Title = info.Title;
            city.Population = info.Population;
            city.Description = info.Description;

            return Ok(city);
        }
    }
}
