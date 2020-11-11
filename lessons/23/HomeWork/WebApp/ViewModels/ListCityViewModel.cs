using System;
using WebApp.CityStorage;

namespace WebApp.ViewModels
{
    public class ListCityViewModel
    {
        public int Page { get; }
        public int Per_page { get; }
        public int Total { get; }
        public int Total_pages { get; }
        public City[] Data { get; }

        public ListCityViewModel(City[] cities, int page, int perPage, int total)
        {
            Page = page;
            Per_page = perPage;
            Total = total;
            Total_pages = CalculateTotalPages(perPage, total);
            Data = cities;
        }

        private int CalculateTotalPages(int perPage, int total)
        {
            return (int)Math.Round((double)total / perPage, MidpointRounding.ToPositiveInfinity);
        }
    }
}
