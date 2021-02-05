using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Reminder.Storage.SqlServer.Tests
{
    public static class DatabaseTestsHelper
    {
        public static string ConnectionString =>
            GetConnectionString();


        public static async Task ExecuteSqlQueryAsync(params string[] scripts)
        {
            await using var connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;

            foreach (var script in scripts)
            {
                command.CommandText = script;
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException)
                {

                }
            }
        }

        private static string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json")
                .Build();
            return configuration.GetConnectionString("ReminderStorageTests");
        }
    }
}
