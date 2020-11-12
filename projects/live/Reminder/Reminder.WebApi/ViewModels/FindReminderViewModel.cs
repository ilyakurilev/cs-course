using System;

namespace Reminder.WebApi.ViewModels
{
    using Reminder.Storage;
    using Reminder.WebApi.ViewModels.Attributes;

    public class FindReminderViewModel
    {
        [CorrectDate(ErrorMessage = "Incorrec date")]
        public DateTimeOffset DateTime { get; set; }
        public ReminderItemStatus Status { get; set; }
    }
}
