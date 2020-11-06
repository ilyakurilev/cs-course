using System;

namespace CitiesWebApp.Model
{
    public class City
    {
		public City()
		{

		}

		public City(Guid id, string title, string description, int population)
		{
			Id = id;
			Title = title;
			Description = description;
			Population = population;
		}

		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Population { get; set; }
	}
}
