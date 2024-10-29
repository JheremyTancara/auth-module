using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation

{
    public class RequiredAttribute : ValidationAttribute
    {
        public RequiredAttribute(string value)
        {
            ErrorMessage = ErrorUtilities.IsRequired(value);
        }
        
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }
    }
}
