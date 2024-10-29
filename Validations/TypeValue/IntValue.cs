using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation

{
    public class IntValue : ValidationAttribute
    {
        public IntValue(string value)
        {
            ErrorMessage = ErrorUtilities.ValidateInt(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is not int)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
