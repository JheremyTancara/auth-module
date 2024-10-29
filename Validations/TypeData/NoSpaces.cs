using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation

{
    public class NoSpaces : ValidationAttribute
    {
        public NoSpaces(string value)
        {
            ErrorMessage = ErrorUtilities.NoSpaces(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string stringValue)
            {
                if (stringValue.Contains(" "))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
