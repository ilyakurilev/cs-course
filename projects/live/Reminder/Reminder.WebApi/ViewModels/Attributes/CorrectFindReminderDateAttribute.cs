using System;
using System.ComponentModel.DataAnnotations;

namespace Reminder.WebApi.ViewModels.Attributes
{
    public class CorrectFindReminderDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateTime = (DateTimeOffset)value;

            if (dateTime != default)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
