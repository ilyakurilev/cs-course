using System;
using System.Collections.Generic;

namespace CitiesWebApp.Model
{
    public class Storage
    {
		public static Storage Instance { get; } =
			new Storage();

		public List<City> Cities { get; }

		private Storage()
		{
			Cities = new List<City>
			{
				new City(Guid.NewGuid(), "Moscow", "The capital of Russia", 16_000_000),
				new City(Guid.NewGuid(), "Tokyo", "The capital of Japan", 15_000_000)
			};
		}
	}
}
