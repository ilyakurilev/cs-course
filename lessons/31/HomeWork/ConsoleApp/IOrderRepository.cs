using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IOrderRepository
	{
		Task<int> Insert(InsertOrderCommand dto);
	}

}
