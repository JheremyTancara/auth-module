using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation

{
    public class StringValue : ValidationAttribute
    {
        public StringValue(string value)
        {
            ErrorMessage = ErrorUtilities.ValidateString(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && !(value is string))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }
    }
}
