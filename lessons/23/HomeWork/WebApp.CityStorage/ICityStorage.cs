using WebApp.Storage;

namespace WebApp.CityStorage
{
    public interface ICityStorage : IStorage<City>
    {
        City FindByTitle(string title);
    }
}
