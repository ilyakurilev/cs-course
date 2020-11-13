using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Reminder.Receiver.Telegram
{
    public class ReminderReceiver : IReminderReceiver
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        private const string ErrorMessage = @"Invalid message format.
Should be as:
<Message>
<DateTimeUtc>
Messages without datetime returns immediately";

        private readonly ITelegramBotClient _client;

        public ReminderReceiver(string token)
        {
            _client = new TelegramBotClient(token);
            _client.OnMessage += OnMessage;
            _client.StartReceiving();
        }

        private void OnMessage(object sender, MessageEventArgs args)
        {
            if (!MessagePayload.TryParse(args.Message.Text, out var message))
            {
                _client.SendTextMessageAsync(args.Message.Chat.Id, ErrorMessage);
                return;
            }

            var contactId = args.Message.Chat.Id;
            var eventArgs = new MessageReceivedEventArgs(message, contactId.ToString());

            MessageReceived?.Invoke(this, eventArgs);
        }
    }
}
