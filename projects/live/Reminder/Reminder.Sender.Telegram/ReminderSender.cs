using System.Net.Http;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Reminder.Sender.Telegram
{
    using Reminder.Sender.Exceptions;

    class ReminderSender : IReminderSender
    {
        private readonly ITelegramBotClient _client;

        public ReminderSender(string token)
        {
            _client = new TelegramBotClient(token);
        }

        public void Send(ReminderNotification item)
        {
            var text = $"{item.Message} at {item.DateTime:R}";
            var chatId = new ChatId(long.Parse(item.ContactId));

            try
            {
                _client.SendTextMessageAsync(chatId, text)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (HttpRequestException exception)
            {
                throw new ReminderSenderException(exception);
            }
        }
    }
}
