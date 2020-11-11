using System.Linq;

namespace WebApp.CityStorage.Memory
{
    public class CitiesStorage : Storage<City>, ICityStorage
    {
        public CitiesStorage() 
            : base()
        {

        }

        public CitiesStorage(params City[] cities)
            : base(cities)
        {

        }

        public City FindByTitle(string title)
        {
            return _list.FirstOrDefault(_ => _.Title == title);
        }
    }
}
