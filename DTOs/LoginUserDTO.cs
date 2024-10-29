using Api.Validation;

namespace Api.DTOs
{
    public class LoginUserDTO
    {
        [Required("Username")]
        [StringValue("Username")]
        [NoSpecialCharacters("Username")]
        [NoSpaces("Username")]
        [LengthRange("Username", 3, 20)]
        public string Username { get; set; } = string.Empty; 
          
        [Required("Password")]
        [StringValue("Password")]
        [PasswordFormat("Password")]
        [LengthRange("Username", 3, 20)]
        public string Password  { get; set; } = string.Empty;
    }
}
