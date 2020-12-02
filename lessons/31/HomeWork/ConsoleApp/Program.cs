using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
	{
		private const string ConnectionString =
			"Server=tcp:shadow-art.database.windows.net,1433;" +
			"Initial Catalog=reminder; " +
			"Persist Security Info=False;" +
			"User ID=i_kurilev@shadow-art;" +
			"Password=3Wuy4V6pPH9HXBgl7zi8;" +
			"Encrypt=True;";

		private static async Task Main(string[] args)
		{
			var productRepository = new ProductRepository(ConnectionString);
			var productsCount = await productRepository.GetCountAsync();
			Console.WriteLine($"Total products: {productsCount}");

			Console.WriteLine("Read all from table Product");

			foreach (var product in await productRepository.GetAllAsync())
			{
				Console.WriteLine(product);
			}

			var id = await productRepository.Insert(
				new InsertProductCommand("Apple Watch 6", 25_000m)
			);
			Console.WriteLine(await productRepository.GetByIdAsync(id));

			var orderRepository = new OrderRepository(ConnectionString);
			var orderId = await orderRepository.Insert(
				new InsertOrderCommand(3)
				{
					Lines = { (1, 3), (2, 5) }
				}
			);
		}
	}

}
