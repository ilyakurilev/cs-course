using System.Net.Http;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace Reminder.Sender.Telegram
{
    using Reminder.Sender.Exceptions;

    public class ReminderSender : IReminderSender
    {
        private readonly ITelegramBotClient _client;

        public ReminderSender(string token)
        {
            _client = new TelegramBotClient(token);
        }

        public async Task SendAsync(ReminderNotification item)
        {
            var text = $"{item.Message} at {item.DateTime:R}";
            var chatId = new ChatId(long.Parse(item.ContactId));

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
