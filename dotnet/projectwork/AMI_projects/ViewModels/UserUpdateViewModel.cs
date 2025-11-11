using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AMI_projects.ViewModels
{
    public class UserUpdateViewModel
    {
        [JsonPropertyName("displayName")]
        [Required]
        public string DisplayName { get; set; }

        [JsonPropertyName("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("password")]
        [MinLength(8)]
        public string? Password { get; set; } // Password is optional on update

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }
}
