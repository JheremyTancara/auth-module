using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation
{
    public class RangeInt : ValidationAttribute
    {
        private readonly int _minValue;
        private readonly int _maxValue;
        private readonly string _value;

        public RangeInt(string value, int minValue, int maxValue)
        {
            _value = value;
            _minValue = minValue;
            _maxValue = maxValue;
            ErrorMessage = ErrorUtilities.IntRangeErrorMessage(value, minValue, maxValue);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int intValue)
            {
                if (intValue < _minValue || intValue > _maxValue)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
