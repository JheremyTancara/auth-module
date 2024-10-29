using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation

{
    public class DoubleValue : ValidationAttribute
    {
        public DoubleValue(string value)
        {
            ErrorMessage = ErrorUtilities.ValidateDouble(value); 
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success!;
            }

            if (value is double)
            {
                return ValidationResult.Success!;
            }

            if (double.TryParse(value.ToString(), out double _))
            {
                return ValidationResult.Success!;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
