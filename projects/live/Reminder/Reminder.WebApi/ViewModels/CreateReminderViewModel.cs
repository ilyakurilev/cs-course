﻿using System;

namespace Reminder.WebApi.ViewModels
{
    using Reminder.Storage;
    using System.ComponentModel.DataAnnotations;

    public class CreateReminderViewModel
    {
        public Guid? Id { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public ReminderItemStatus Status { get; set; }
        
        [Required]
        [StringLength(512)]
        public string Message { get; set; }

        [Required]
        [StringLength(32)]
        public string ContactId { get; set; }
    }
}
