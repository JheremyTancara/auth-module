using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation

{
    public class GmailFormat : ValidationAttribute
    {
        private readonly string _value;

        public GmailFormat(string value)
        {
            _value = value;
            ErrorMessage = ErrorUtilities.InvalidGmailFormat(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string email)
            {
                // Verificar que el email termina en @gmail.com
                if (!email.EndsWith("@gmail.com"))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
