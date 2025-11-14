using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SearchTicketApp.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GreaterThanDateAttribute : ValidationAttribute
    {
        private readonly string _otherDatePropertyName;

        public GreaterThanDateAttribute(string otherDatePropertyName)
        {
            _otherDatePropertyName = otherDatePropertyName ?? throw new ArgumentNullException(nameof(otherDatePropertyName));
        }

        private static string GetErrorMessage(string firstDateName, string secondDateName) =>
            $"Date '{firstDateName}' must be greater than date '{secondDateName}'.";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (!(value is DateTime firstDate))
            {
                throw new InvalidOperationException($"The property {validationContext.MemberName} is not a DateTime.");
            }

            var otherProperty = validationContext.ObjectType.GetProperty(_otherDatePropertyName,
                BindingFlags.Public | BindingFlags.Instance);

            if (otherProperty == null || otherProperty.PropertyType != typeof(DateTime))
            {
                throw new InvalidOperationException($"Unknown property: {_otherDatePropertyName} or wrong type.");
            }

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);
            if (otherValue == null) return ValidationResult.Success;

            var secondDate = (DateTime)otherValue;

            return firstDate > secondDate
                ? ValidationResult.Success
                : new ValidationResult(GetErrorMessage(validationContext.MemberName!, _otherDatePropertyName));
        }
    }

}
