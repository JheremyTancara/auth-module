using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Validation
{
  public class WithinListEnumValues : ValidationAttribute
  {
    private readonly Type _enumType;

    public WithinListEnumValues(Type enumType, string value)
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
            var values = stringValue.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(s => s.Trim())
                                    .ToArray();

            if (values.Length == 0)
            {
                return new ValidationResult("The string does not contain valid enum values.");
            }

            foreach (var val in values)
            {
                if (!Enum.IsDefined(_enumType, val))
                {
                    return new ValidationResult($"'{val}' is not a valid value for {_enumType.Name}.");
                }
            }
        }
        return ValidationResult.Success!;
    }
  }
}
