using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IProductRepository
	{
		Task<Product> GetByIdAsync(int id);
		Task<Product[]> GetAllAsync();
		Task<int> GetCountAsync();
		Task<int> Insert(InsertProductCommand command);
	}

}
