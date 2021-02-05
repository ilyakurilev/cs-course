using System;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Reminder.Storage.SqlServer
{
    using Exceptions;
    using System.Collections.Generic;

    public class ReminderStorage : IReminderStorage
    {
        private readonly string _connectionString;

        public ReminderStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(ReminderItem item)
        {
            await using var connection = await GetConnectionAsync();
            await using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddReminderItem";
            command.Parameters.AddWithValue("id", item.Id);
            command.Parameters.AddWithValue("message", item.Message);
            command.Parameters.AddWithValue("chatId", item.ChatId);
            command.Parameters.AddWithValue("dateTime", item.DateTime);
            command.Parameters.AddWithValue("statusId", (int)item.Status);

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException exception) when (exception.Number == 2627)
            {
                throw new ReminderItemAlreadyExistsException(item.Id);
            }
        }

        public async Task<ReminderItem[]> FindAsync(ReminderItemFilter filter)
        {
            await using var connection = await GetConnectionAsync();
            await using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;

            var query = @"
SELECT R.Id,
       R.StatusId,
       R.DateTime,
       R.Message,
       RC.ChatId
  FROM [ReminderItems] AS R
  JOIN [ReminderContacts] AS RC
    ON R.ContactId = RC.Id
";

            var conditions = new List<string>();
            if (filter.DateTime.HasValue)
            {
                conditions.Add("R.DateTime <= @dateTime");
                command.Parameters.AddWithValue("dateTime", filter.DateTime);
            }
            if (filter.Status.HasValue)
            {
                conditions.Add("R.StatusId = @statusId");
                command.Parameters.AddWithValue("statusId", filter.Status);
            }
            if (conditions.Count > 0)
            {
                query += "WHERE ";
                query += string.Join(" AND ", conditions);
            }
            command.CommandText = query;

            var found = await ReadReminderAsync(command).ToArrayAsync();
            
            return found;
        }

        public async Task<ReminderItem> GetAsync(Guid id)
        {
            await using var connection = await GetConnectionAsync();
            await using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
SELECT R.Id,
       R.StatusId,
       R.DateTime,
       R.Message,
       RC.ChatId
  FROM [ReminderItems] AS R
  JOIN [ReminderContacts] AS RC
    ON R.ContactId = RC.Id
 WHERE R.Id = @id;
";
            command.Parameters.AddWithValue("id", id);

            var item = await ReadReminderAsync(command).FirstOrDefaultAsync();
            if (item is null)
            {
                throw new ReminderItemNotFoundException(id);
            }

            return item;
        }

        public async Task UpdateAsync(ReminderItem item)
        {
            await using var connection = await GetConnectionAsync();
            await using var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UpdateReminderItem";
            command.Parameters.AddWithValue("id", item.Id);
            command.Parameters.AddWithValue("message", item.Message);
            command.Parameters.AddWithValue("statusId", (int)item.Status);
            var rows = command.Parameters.Add("rows", SqlDbType.Int);
            rows.Direction = ParameterDirection.Output;

            await command.ExecuteNonQueryAsync();

            if((int)rows.Value == 0)
            {
                throw new ReminderItemNotFoundException(item.Id);
            }
        }

        private async Task<SqlConnection> GetConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        private async IAsyncEnumerable<ReminderItem> ReadReminderAsync(SqlCommand command)
        {
            await using var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
            {
                yield break;
            }

            var idColumn = reader.GetOrdinal("Id");
            var statusIdColumn = reader.GetOrdinal("StatusId");
            var dateTimeColumn = reader.GetOrdinal("DateTime");
            var messageColumn = reader.GetOrdinal("Message");
            var chatIdColumn = reader.GetOrdinal("ChatId");

            while (await reader.ReadAsync())
            {
                yield return new ReminderItem(
                reader.GetGuid(idColumn),
                (ReminderItemStatus)reader.GetInt32(statusIdColumn),
                reader.GetDateTimeOffset(dateTimeColumn),
                reader.GetString(messageColumn),
                reader.GetString(chatIdColumn));
            }
        }
    }
}
