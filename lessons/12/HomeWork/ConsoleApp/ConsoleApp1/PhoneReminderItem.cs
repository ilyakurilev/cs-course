using System;

namespace ConsoleApp1
{
    class PhoneReminderItem : ReminderItem
    {
        public string PhoneNumber { get; set; }

        public PhoneReminderItem(DateTimeOffset alarmDate, string alarmMessage, string phoneNumber) :
            base(alarmDate, alarmMessage)
        {
            PhoneNumber = phoneNumber;
        }

        public override void WriteProperties()
        {
            base.WriteProperties();
            Console.WriteLine($"PhoneNumber: {PhoneNumber}");
        }
    }
}
