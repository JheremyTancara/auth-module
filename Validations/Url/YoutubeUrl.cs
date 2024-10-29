using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Api.Utilities;

namespace Api.Validation

{
    public class YouTubeUrl : ValidationAttribute
    {
        private readonly string _propertyName;

        public YouTubeUrl(string propertyName)
        {
            _propertyName = propertyName;
            ErrorMessage = ErrorUtilities.InvalidYouTubeUrl(propertyName); 
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string url)
            {
                var regex = new Regex(@"^(https?:\/\/)?(www\.)?(youtube\.com\/(watch\?v=|embed\/|v\/|.+\?.+v=)|youtu\.be\/)[\w-]{11}(\?.*)?$", RegexOptions.IgnoreCase);

                if (!regex.IsMatch(url))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success!;
        }
    }
}
