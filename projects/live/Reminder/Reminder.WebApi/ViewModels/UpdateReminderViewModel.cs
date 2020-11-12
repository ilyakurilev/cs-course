using System.ComponentModel.DataAnnotations;

namespace Reminder.WebApi.ViewModels
{
    using Reminder.Storage;

    public class UpdateReminderViewModel
    {
        public ReminderItemStatus Status { get; set; }

        [Required]
        [StringLength(512)]
        public string Message { get; set; }
    }
}
