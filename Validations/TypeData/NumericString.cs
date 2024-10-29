using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Api.Utilities;

namespace Api.Validation
{
    public class NumericString : ValidationAttribute
    {
        public NumericString(string fieldName)
        {
            ErrorMessage = ErrorUtilities.ValidateNumericString(fieldName);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string input)
            {
                var regex = new Regex(@"^\d+$");

                if (!regex.IsMatch(input))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
