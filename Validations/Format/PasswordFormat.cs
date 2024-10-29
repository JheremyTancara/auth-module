using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Api.Utilities;

namespace Api.Validation
{
    public class PasswordFormat : ValidationAttribute
    {
        public PasswordFormat(string value)
        {
            ErrorMessage = ErrorUtilities.InvalidPasswordFormat(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string password)
            {
                var hasSpecialChar = new Regex(@"[!@#$%^&*(),.?""{}|<>]");
                var hasNumber = new Regex(@"\d");
                var hasUpperCase = new Regex(@"[A-Z]");

                int upperCaseCount = 0;
                foreach (char c in password)
                {
                    if (char.IsUpper(c))
                    {
                        upperCaseCount++;
                    }
                }

                if (!hasSpecialChar.IsMatch(password) || 
                    !hasNumber.IsMatch(password) || 
                    upperCaseCount < 2)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
