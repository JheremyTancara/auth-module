using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Api.Utilities;

namespace Api.Validation

{
    public class TimeFormat : ValidationAttribute
    {
        private readonly string _propertyName;

        public TimeFormat(string propertyName)
        {
            _propertyName = propertyName;
            ErrorMessage = ErrorUtilities.InvalidTimeFormat(propertyName);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string timeString)
            {
                var regex = new Regex(@"^(?:[01]\d|2[0-3]):[0-5]\d$", RegexOptions.IgnoreCase);

                if (!regex.IsMatch(timeString))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
