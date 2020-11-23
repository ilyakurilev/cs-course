using System;
using System.ComponentModel.DataAnnotations;

namespace Reminder.WebApi.ViewModels.Attributes
{
    public class CorrectCreateReminderDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateTime = (DateTimeOffset)value;

            if (dateTime < DateTimeOffset.UtcNow)
            {
                return new ValidationResult("DateTime must be in future or now");
            }

            return ValidationResult.Success;
        }
    }
}
