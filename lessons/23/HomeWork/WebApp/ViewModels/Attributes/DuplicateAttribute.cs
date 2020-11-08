using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WebApp.ViewModels.Attributes
{
    public class DuplicateAttribute : ValidationAttribute
    {
		public string PropertyName { get; }

		public DuplicateAttribute(string propertyName)
		{
			PropertyName = propertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var propertyInfo = validationContext
				.ObjectType
				.GetProperty(PropertyName, BindingFlags.Public | BindingFlags.Instance);
			if (propertyInfo == null)
			{
				return ValidationResult.Success;
			}

			var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance);
			var first = propertyValue?.ToString()?.Trim();
			var second = value?.ToString()?.Trim();

			if (!string.Equals(first, second, StringComparison.OrdinalIgnoreCase))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
		}
	}
}
