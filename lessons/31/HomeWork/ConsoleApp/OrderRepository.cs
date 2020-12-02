using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class OrderRepository : IOrderRepository
	{
		private readonly string _connection;

		public OrderRepository(string connection)
		{
			_connection = connection;
		}

		public async Task<int> Insert(InsertOrderCommand dto)
		{
			// insert -> Orders
			// OrderId 
			// insert -> OrderLines

			await using var connection = await GetConnection();
			await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

			var command = connection.CreateCommand();
			command.CommandText = "InsertOrder";
			command.CommandType = CommandType.StoredProcedure;
			command.Transaction = transaction;
			command.Parameters.AddWithValue("p_customerId", dto.CustomerId);
			command.Parameters.AddWithValue("p_orderDate", dto.OrderDate);
			command.Parameters.AddWithValue("p_discount", dto.Discount.HasValue ? (object)dto.Discount.Value : DBNull.Value);
			var orderId = command.Parameters.Add("p_id", SqlDbType.Int);
			orderId.Direction = ParameterDirection.Output;

			try
			{
				await command.ExecuteNonQueryAsync();

				foreach (var (productId, count) in dto.Lines)
				{
					command = connection.CreateCommand();
					command.CommandText = "INSERT INTO [OrderLine] (OrderId, ProductId, Count) VALUES(@orderId, @productId, @count)";
					command.CommandType = CommandType.Text;
					command.Transaction = transaction;
					command.Parameters.AddWithValue("orderId", (int)orderId.Value);
					command.Parameters.AddWithValue("productId", productId);
					command.Parameters.AddWithValue("count", count);

					await command.ExecuteNonQueryAsync();
				}

				await transaction.CommitAsync();
			}
			catch (SqlException)
			{
				await transaction.RollbackAsync();
				throw;
			}

			return (int)orderId.Value;
		}

		private async Task<SqlConnection> GetConnection()
		{
			var connection = new SqlConnection(_connection);
			await connection.OpenAsync();
			return connection;
		}
	}

}
