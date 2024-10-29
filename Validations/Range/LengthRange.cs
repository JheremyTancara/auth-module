using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation
{
    public class LengthRange : ValidationAttribute
    {
        private readonly string _propertyName;
        private readonly int _minLength;
        private readonly int _maxLength;

        public LengthRange(string propertyName, int minLength, int maxLength)
        {
            _propertyName = propertyName;
            _minLength = minLength;
            _maxLength = maxLength;
            ErrorMessage = ErrorUtilities.LengthRangeErrorMessage(propertyName, minLength, maxLength);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string stringValue)
            {
                int length = stringValue.Length;

                if (length < _minLength || length > _maxLength)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
