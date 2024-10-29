using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Api.Utilities;

namespace Api.Validation

{
    public class DateFormat : ValidationAttribute
    {
        public DateFormat(string value)
        {
            ErrorMessage = ErrorUtilities.InvalidDateFormat(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string dateString)
            {
                if (!DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
