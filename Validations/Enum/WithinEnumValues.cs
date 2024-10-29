using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation

{
    public class WithinEnumValues : ValidationAttribute
    {
        private readonly Type _enumType;

        public WithinEnumValues(Type enumType, string value)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException("Provided type must be an enum.", nameof(enumType));

            _enumType = enumType;
            ErrorMessage = ErrorUtilities.InvalidEnumValue(value);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string stringValue)
            {
                var enumNames = Enum.GetNames(_enumType);
                if (!enumNames.Contains(stringValue))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success!;
        }
    }
}
