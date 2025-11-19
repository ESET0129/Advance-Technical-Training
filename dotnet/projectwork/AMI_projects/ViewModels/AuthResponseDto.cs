using System.Text.Json.Serialization;

namespace AMI_projects.ViewModels
{
    public class AuthResponseDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}