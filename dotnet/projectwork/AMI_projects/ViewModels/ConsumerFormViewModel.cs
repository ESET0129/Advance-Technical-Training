using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AMI_projects.ViewModels
{
    public class ConsumerFormViewModel
    {
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("email")]
        [EmailAddress]
        public string? Email { get; set; }

        [JsonPropertyName("status")]
        [Required]
        public string Status { get; set; }
    }
}