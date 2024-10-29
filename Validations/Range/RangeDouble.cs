using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation

{
    public class RangeDouble : ValidationAttribute
    {
        private readonly double _min;
        private readonly double _max;

        public RangeDouble(string value, double min, double max)
        {
            _min = min;
            _max = max;
            ErrorMessage = ErrorUtilities.DoubleRangeErrorMessage(value, _min, _max);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is double doubleValue)
            {
                if (doubleValue < _min || doubleValue > _max)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success!;
        }
    }
}
