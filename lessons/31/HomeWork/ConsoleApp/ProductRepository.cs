using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ProductRepository : IProductRepository
	{
		private readonly string _connection;

		public ProductRepository(string connection)
		{
			_connection = connection;
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			await using var connection = await GetConnection();

			var command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			// command.CommandText = $"SELECT * FROM [Product] WHERE Id = {}";
			command.CommandText = "SELECT * FROM [Product] WHERE Id = @id";
			command.Parameters.AddWithValue("id", id);

			await using var reader = await command.ExecuteReaderAsync();

			if (!reader.HasRows)
			{
				throw new ArgumentException($"Product with id {id} not found");
			}

			var idIndex = reader.GetOrdinal("Id");
			var nameIndex = reader.GetOrdinal("Name");
			var priceIndex = reader.GetOrdinal("Price");

			await reader.ReadAsync();

			var product = new Product(
				reader.GetInt32(idIndex),
				reader.GetString(nameIndex),
				reader.GetDecimal(priceIndex)
			);
			return product;
		}

		public async Task<Product[]> GetAllAsync()
		{
			await using var connection = await GetConnection();

			var command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = "SELECT * FROM [Product]";

			await using var reader = await command.ExecuteReaderAsync();

			if (!reader.HasRows)
			{
				return Array.Empty<Product>();
			}

			var products = new List<Product>();
			var idIndex = reader.GetOrdinal("Id");
			var nameIndex = reader.GetOrdinal("Name");
			var priceIndex = reader.GetOrdinal("Price");

			while (await reader.ReadAsync())
			{
				var product = new Product(
					reader.GetInt32(idIndex),
					reader.GetString(nameIndex),
					reader.GetDecimal(priceIndex)
				);
				products.Add(product);
			}

			return products.ToArray();
		}

		public async Task<int> GetCountAsync()
		{
			await using var connection = await GetConnection();
			var command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = "SELECT COUNT(*) FROM [Product]";

			return (int)command.ExecuteScalar();
		}

		public async Task<int> Insert(InsertProductCommand dto)
		{
			await using var connection = await GetConnection();
			var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "InsertProduct";
			command.Parameters.AddWithValue("p_name", dto.Name);
			command.Parameters.AddWithValue("p_price", dto.Price);
			var parameter = command.Parameters.Add("p_id", SqlDbType.Int);
			parameter.Direction = ParameterDirection.Output;

			await command.ExecuteNonQueryAsync();

			return (int)parameter.Value;
		}

		private async Task<SqlConnection> GetConnection()
		{
			var connection = new SqlConnection(_connection);
			await connection.OpenAsync();
			return connection;
		}
	}

}
