﻿using System;

namespace Reminder.WebApi.ViewModels
{
    using Reminder.Storage;
    using Reminder.WebApi.ViewModels.Attributes;

    public class FindReminderViewModel
    {
        [CorrectFindReminderDate(ErrorMessage = "Invalid DateTime format")]
        public DateTimeOffset DateTime { get; set; }
        public ReminderItemStatus Status { get; set; }
    }
}
