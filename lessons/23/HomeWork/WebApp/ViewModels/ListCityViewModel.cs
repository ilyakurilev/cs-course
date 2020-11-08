using System;
using System.Linq;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ListCityViewModel
    {
        public int Page { get; }
        public int Per_page { get; }
        public int Total { get; }
        public int Total_pages { get; }
        public City[] Data { get; }

        public ListCityViewModel(City[] cities, int page, int perPage)
        {
            Page = page;
            Per_page = perPage;
            Total = cities.Length;
            Total_pages = (int) Math.Round((double)cities.Length / perPage, MidpointRounding.ToPositiveInfinity);
            Data = GetDataFromCities(cities);
        }

        private City[] GetDataFromCities(City[] cities)
        {
            var index = (Page - 1) * Per_page;
            var endIndex = index + Per_page - 1;

            return cities.TakeWhile(_ => index++ < endIndex)?.ToArray();
        }
    }
}
