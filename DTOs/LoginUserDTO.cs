using System.Text.Json.Serialization;

namespace Api.DTOs
{
    public class LoginUserDTO
    {
        public string Username { get; set; } = string.Empty;    
        public string Password  { get; set; } = string.Empty;
    }
}
