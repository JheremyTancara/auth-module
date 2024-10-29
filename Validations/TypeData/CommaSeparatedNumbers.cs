using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Api.Utilities;

namespace Api.Validation
{
    public class CommaSeparatedNumbers : ValidationAttribute
    {
        public CommaSeparatedNumbers(string fieldName)
        {
            ErrorMessage = ErrorUtilities.ValidateCommaSeparatedNumbers(fieldName);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string input)
            {
                var regex = new Regex(@"^(?!.*,$)(\d+\s*,?\s*)*$");

                if (!regex.IsMatch(input))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
