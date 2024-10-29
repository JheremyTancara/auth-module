using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Api.Utilities;

namespace Api.Validation

{
    public class NoSpecialCharacters : ValidationAttribute
    {
        public NoSpecialCharacters(string value)
        {
            ErrorMessage = ErrorUtilities.NoSpecialCharacters(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string stringValue)
            {
                var regex = new Regex("^[a-zA-Z0-9 ]*$");

                if (!regex.IsMatch(stringValue))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
