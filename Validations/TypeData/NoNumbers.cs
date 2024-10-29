using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Api.Utilities;

namespace Api.Validation

{
    public class NoNumbers : ValidationAttribute
    {
        public NoNumbers(string value)
        {
            ErrorMessage = ErrorUtilities.NoNumbers(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string stringValue)
            {
                var regex = new Regex("^[a-zA-Z ]*$");

                if (!regex.IsMatch(stringValue))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
