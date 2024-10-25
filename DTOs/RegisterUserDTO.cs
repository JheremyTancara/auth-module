using System.Text.Json.Serialization;

namespace Api.DTOs

{
    public class RegisterUserDTO
    {
        [JsonIgnore]
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;    

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string DateOfBirth { get; set; } = string.Empty;

        public string SubscriptionLevel { get; set; } = string.Empty;

        public string ProfilePicture { get; set; } = string.Empty;
    }
}
