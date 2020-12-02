using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ConsoleApp1
{
    class Program
    {
        private const string ConnectionString =
			"Server=tcp:shadow-art.database.windows.net,1433;" +
			"Initial Catalog=reminder; " +
			"Persist Security Info=False;" +
            "User ID=i_kurilev@shadow-art;" +
            "Password=3Wuy4V6pPH9HXBgl7zi8;" +
			"Encrypt=True;";

        public class Order
        {
            public int Id { get; set; }
            public string Customer { get; set; }
            public DateTimeOffset OrderDate { get; set; }
            public double Discount { get; set; }

            public Order(int id, string customer, DateTimeOffset orderDate, double discount)
            {
                Id = id;
                Customer = customer;
                OrderDate = orderDate;
                Discount = discount;
            }

            public override string ToString() =>
                $"Order id = {Id}, Customer = {Customer}, OrderDate = {OrderDate}";
            
        }

        public interface IOrderRepository
        {
            Task<int> GetCount();
            Task<Order> GetById(int id);
            Task<List<Order>> GetAll();
        }

        public class OrderRepository : IOrderRepository
        {
            private readonly string _connection;

            public OrderRepository(string connection)
            {
                _connection = connection;
            }

            public Task<List<Order>> GetAll()
            {
                throw new NotImplementedException();
            }

            public async Task<Order> GetById(int id)
            {
                await using var connection = await GetConnection();
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT " +
                                        "O.Id, " +
                                        "C.Name, " +
                                        "O.OrderDate, " +
                                        "O.Discount " +
                                      "FROM [Order] AS O " +
                                      "JOIN [Customer] AS C ON O.CustomerId = C.Id " +
                                      "WHERE O.Id = @id";
                command.Parameters.AddWithValue("id", id);

                await using var reader = await command.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    throw new ArgumentException($"Order with id = {id} not found");
                }

                var idIndex = reader.GetOrdinal("Id");
                var nameIndex = reader.GetOrdinal("Name");
                var orderDateIndex = reader.GetOrdinal("OrderDate");
                var discountIndex = reader.GetOrdinal("Discount");

                await reader.ReadAsync();

                var order = new Order(reader.GetInt32(idIndex),
                    reader.GetString(nameIndex),
                    reader.GetDateTimeOffset(orderDateIndex),
                    reader.GetDouble(discountIndex));
                return order;
            }

            public async Task<int> GetCount()
            {
                await using var connection = await GetConnection();
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT COUNT(*) FROM [Order]";

                var count = (int) await command.ExecuteScalarAsync();
                return count;
            }

            private async Task<SqlConnection> GetConnection()
            {
                var connection = new SqlConnection(_connection);
                await connection.OpenAsync();
                return connection;
            }
        }

        static async Task Main(string[] args)
        {
            var orderRepository = new OrderRepository(ConnectionString);
            Console.WriteLine(await orderRepository.GetCount());
            Console.WriteLine(await orderRepository.GetById(2));
        }
    }
}
