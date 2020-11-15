using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Reminder.Storage.WebApi
{
    using Reminder.Storage.Exceptions;
    using Reminder.Storage.WebApi.Dto;
    using System.Linq;

    class ReminderStorage : IReminderStorage
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

        public void Add(ReminderItem item)
        {
            var json = JsonSerializer.Serialize(item, SerializerOptions);
            var content = new StringContent(json);
            
            var response = _client.PostAsync(ApiPrefix, content)
                .GetAwaiter()
                .GetResult();
            
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ReminderItemAlreadyExistsException(item.Id);
            }

            response.EnsureSuccessStatusCode();
        }

        public ReminderItem[] Find(DateTimeOffset dateTime, ReminderItemStatus status = ReminderItemStatus.Created)
        {
            var response = _client.GetAsync($"{ApiPrefix}?dateTime={dateTime}&status={status}")
                .GetAwaiter()
                .GetResult();

            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

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

        public ReminderItem Get(Guid id)
        {
            var response = _client.GetAsync($"{ApiPrefix}/{id:N}")
                .GetAwaiter()
                .GetResult();

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ReminderItemNotFoundException(id);
            }

            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();
            var dto = JsonSerializer.Deserialize<ReminderItemDto>(json, SerializerOptions);

            return new ReminderItem(
                dto.Id,
                dto.Status,
                dto.DateTime,
                dto.Message,
                dto.ContactId);
        }

        public void Update(ReminderItem item)
        {
            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json);

            var response = _client.PutAsync($"{ApiPrefix}/{item.Id:N}", content)
                .GetAwaiter()
                .GetResult();

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ReminderItemNotFoundException(item.Id);
            }

            response.EnsureSuccessStatusCode();
        }
    }
}
