using System.Text.Json.Serialization;

namespace AMI_projects.ViewModels
{
    public class TariffViewModel
    {
        [JsonPropertyName("tariffId")]
        public int TariffId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("effectiveFrom")]
        public DateOnly EffectiveFrom { get; set; }

        [JsonPropertyName("effectiveTo")]
        public DateOnly? EffectiveTo { get; set; }

        [JsonPropertyName("baseRate")]
        public decimal BaseRate { get; set; }

        [JsonPropertyName("taxRate")]
        public decimal TaxRate { get; set; }
    }
}
