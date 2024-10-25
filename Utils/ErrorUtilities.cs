using Microsoft.AspNetCore.Mvc;

namespace Api.Utilities

{
  public static class ErrorUtilities
  {
    public static NotFoundObjectResult FieldNotFound(string value, int id)
    {
        return new NotFoundObjectResult(new { message = $"The {value} with ID = {id} doesn't exist." });
    }

    public static string valueNotFound(string entity, string value)
    {
        return $"{entity} with the field '{value}'";
    }

    public static NotFoundObjectResult IdPositive(int id)
    {
        return new NotFoundObjectResult(new { message = $"ID = {id} must be a positive value greater than 0." });
    }

    public static NotFoundObjectResult UniqueName(string value)
    {
        return new NotFoundObjectResult(new { message = $"The {value} name already exists. Provide a unique name." });
    }

    public static NotFoundObjectResult EmailName(string value)
    {
        return new NotFoundObjectResult(new { message = $"The {value} email already exists. Provide a unique email." });
    }

    public static string ValidateString(string value) => $"This field needs ({value}) to be a String.";

    public static string ValidateInt(string value) => $"This field needs ({value}) to be a Int.";

    public static string ValidateDouble(string value) => $"The field '{value}' must be a valid double value.";

    public static string IntRangeErrorMessage(string value, int minValue, int maxValue) => $"The value for {value} must be between {minValue} and {maxValue}.";

    public static string RangeValueErrorMessageDecimal(double range1, double range2) => $"The number must be greater than or equal to ({range1}) and less than ({range2}).";

    public static string PositiveNumber(string value) => $"This field ({value}) must be a positive number.";

    public static string IsRequired(string value) => $"This field ({value}) is required.";

    public static string NoSpecialCharacters(string value) => $"The ({value}) field must not contain special characters";

    public static string NoNumbers(string value) => $"The ({value}) field must not contain numbers";

    public static string NoSpaces(string value) => $"The ({value}) field must not contain spaces";

    public static string InvalidImageUrl(string value)
    {
        return $"The provided URL for {value} is not valid. Please ensure it is a valid image URL (e.g., .png, .jpg, .gif, .bmp).";
    }

    public static string InvalidEnumValue(string value)
    {
        return $"{value} is not a valid value.";
    }

    public static string InvalidYouTubeUrl(string value)
    {
        return $"Please provide a valid YouTube URL for {value}.";
    }

    public static string InvalidTimeFormat(string value)
    {
        return $"Please provide a valid time format (HH:mm:ss) for {value}.";
    }

    public static string InvalidDateFormat(string value)
    {
        return $"Please provide a valid date format (dd/MM/yyyy) for {value}.";
    }

    public static string InvalidPasswordFormat(string value)
    {
        return $"The password for {value} must contain at least one special character, at least one number, and at least two uppercase letters.";
    }

    public static string InvalidGmailFormat(string value)
    {
        return $"{value} must be a valid Gmail address ending with '@gmail.com'.";
    }

    public static string LengthRangeErrorMessage(string value, int minLength, int maxLength)
    {
        return $"The length of '{value}' must be between {minLength} and {maxLength} characters.";
    }

    public static string DoubleRangeErrorMessage(string value, double min, double max)
    {
        return $"The field '{value}' must be between {min} and {max}.";
    }

    public static string ValidateCommaSeparatedNumbers(string value) => 
    $"{value} has an invalid format. It must be a list of integers separated by commas (e.g., 2, 3, 3) without trailing commas.";

    public static string ValidateNumericString(string value) =>
    $"{value} must contain only numbers (e.g., 100, 20321).";
  }
}
