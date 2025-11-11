using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AMI_projects.ViewModels
{
    public class TariffFormViewModel
    {
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("effectiveFrom")]
        [Required]
        public DateOnly EffectiveFrom { get; set; }

        [JsonPropertyName("effectiveTo")]
        public DateOnly? EffectiveTo { get; set; }

        [JsonPropertyName("baseRate")]
        [Required]
        public decimal BaseRate { get; set; }

        [JsonPropertyName("taxRate")]
        public decimal TaxRate { get; set; } = 0;
    }
}
