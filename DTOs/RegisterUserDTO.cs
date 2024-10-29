using System.Text.Json.Serialization;
using Api.Models;
using Api.Validation;

namespace Api.DTOs

{
    public class RegisterUserDTO
    {
        [JsonIgnore]
        public int UserID { get; set; }

        [Required("Username")]
        [StringValue("Username")]
        [NoSpecialCharacters("Username")]
        [NoSpaces("Username")]
        [LengthRange("Username", 3, 20)]
        public string Username { get; set; } = string.Empty;    

        [Required("Email")]
        [StringValue("Email")]
        [GmailFormat("Email")]
        [LengthRange("Email", 3, 256)]
        public string Email { get; set; } = string.Empty;

        [Required("Password")]
        [StringValue("Password")]
        [PasswordFormat("Password")]
        [LengthRange("Username", 3, 20)]
        public string Password { get; set; } = string.Empty;

        [Required("DateOfBirth")]
        [StringValue("DateOfBirth")]
        [NoSpaces("DateOfBirth")]
        [DateFormat("DateOfBirth")]
        public string DateOfBirth { get; set; } = string.Empty;

        [Required("SubscriptionLevel")]
        [StringValue("SubscriptionLevel")]
        [NoSpecialCharacters("SubscriptionLevel")]
        [WithinEnumValues(typeof(SubscriptionLevel), "SubscriptionLevel")]
        public string SubscriptionLevel { get; set; } = string.Empty;

        [Required("ProfilePicture")]
        [StringValue("ProfilePicture")]
        [NoSpecialCharacters("ProfilePicture")]
        [WithinEnumValues(typeof(ProfileSkin), "ProfilePicture")]
        public string ProfilePicture { get; set; } = string.Empty;
    }
}
