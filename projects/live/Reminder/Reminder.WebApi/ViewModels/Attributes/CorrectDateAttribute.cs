using System;
using System.ComponentModel.DataAnnotations;

namespace Reminder.WebApi.ViewModels.Attributes
{
    public class CorrectDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (DateTimeOffset.TryParse(value.ToString(), out _))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
