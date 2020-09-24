using System;

namespace ConsoleApp1
{
    class ChatReminderItem : ReminderItem
    {
        public string ChatName { get; set; }
        public string AccountName { get; set; }

        public ChatReminderItem(DateTimeOffset alarmDate, string alarmMessage, string chatName, string accountName) :
            base(alarmDate, alarmMessage)
        {
            ChatName = chatName;
            AccountName = accountName;
        }

        public override void WriteProperties()
        {
            base.WriteProperties();
            Console.WriteLine($"ChatName: {ChatName}");
            Console.WriteLine($"AccountName: {AccountName}");
        }
    }
}
