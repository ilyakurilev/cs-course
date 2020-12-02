using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reminder.Storage.WebApi
{
    using Reminder.Storage.Exceptions;
    using Reminder.Storage.WebApi.Dto;

    public class ReminderStorage : IReminderStorage
    {
        private const string ApiPrefix = "/api/reminders";

        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HttpClient _client;

        public ReminderStorage(string endpoint)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };
        }

        public ReminderStorage(HttpClient client)
        {
            _client = client;
        }

        public async Task AddAsync(ReminderItem item)
        {
            var json = JsonSerializer.Serialize(item, SerializerOptions);
            var content = new StringContent(json, Encoding.Unicode, "application/json");

            var response = await _client.PostAsync(ApiPrefix, content);
            
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ReminderItemAlreadyExistsException(item.Id);
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task<ReminderItem[]> FindAsync(DateTimeOffset dateTime, ReminderItemStatus status = ReminderItemStatus.Created)
        {
            var response = await _client.GetAsync($"{ApiPrefix}?dateTime={dateTime:u}&status={status}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var dtoItems = JsonSerializer.Deserialize<ReminderItemDto[]>(json, SerializerOptions);

            return dtoItems
                .Select(dto => new ReminderItem(
                    dto.Id,
                    dto.Status,
                    dto.DateTime,
                    dto.Message,
                    dto.ContactId)
                )
                .ToArray();
        }

        public async Task<ReminderItem> GetAsync(Guid id)
        {
            var response = await _client.GetAsync($"{ApiPrefix}/{id:N}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ReminderItemNotFoundException(id);
            }

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<ReminderItemDto>(json, SerializerOptions);

            return new ReminderItem(
                dto.Id,
                dto.Status,
                dto.DateTime,
                dto.Message,
                dto.ContactId);
        }

        public async Task UpdateAsync(ReminderItem item)
        {
            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.Unicode, "application/json");

            var response = await _client.PutAsync($"{ApiPrefix}/{item.Id:N}", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ReminderItemNotFoundException(item.Id);
            }

            response.EnsureSuccessStatusCode();
        }
    }
}
