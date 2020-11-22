using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Reminder.Sender.Telegram
{
    using Reminder.Sender;
    using Reminder.Sender.Exceptions;

    public class AsyncReminderSender : IAsyncReminderSender
    {
        private readonly ITelegramBotClient _client;

        public AsyncReminderSender(string token)
        {
            _client = new TelegramBotClient(token);
        }

        public async Task SendAsync(ReminderNotification notification)
        {
            var text = $"{notification.Message} at {notification.DateTime:R}";
            var chatId = new ChatId(long.Parse(notification.ContactId));

            try
            {
                await _client.SendTextMessageAsync(chatId, text);
            }
            catch (HttpRequestException exception)
            {
                throw new ReminderSenderException(exception);
            }
        }
    }
}
