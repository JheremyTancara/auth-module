using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Api.Utilities;

namespace Api.Validation

{
    public class ImageUrl : ValidationAttribute
    {
        private readonly string _propertyName;

        public ImageUrl(string propertyName)
        {
            _propertyName = propertyName;
            ErrorMessage = ErrorUtilities.InvalidImageUrl(propertyName); 
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string url)
            {
                var regex = new Regex(@"^(https?:\/\/.*\.(?:png|jpg|jpeg|gif|bmp))$", RegexOptions.IgnoreCase);

                if (!regex.IsMatch(url))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
