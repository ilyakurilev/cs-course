using System;
using System.ComponentModel.DataAnnotations;

namespace Reminder.WebApi.ViewModels.Attributes
{
    public class CorrectCreateReminderDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (DateTimeOffset.TryParse(value.ToString(), out var dateTime))
            {
                var now = DateTimeOffset.UtcNow;
                if (dateTime < now)
                {
                    return new ValidationResult("DateTime must be in future or now");
                }
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid DateTime format");
        }
    }
}
