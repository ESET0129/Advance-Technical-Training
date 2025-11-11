using System.Text.Json.Serialization;

namespace AMI_projects.ViewModels
{
    public class TariffSlabViewModel
    {
        [JsonPropertyName("tariffSlabId")]
        public int TariffSlabId { get; set; }

        [JsonPropertyName("tariffId")]
        public int TariffId { get; set; }

        [JsonPropertyName("fromKwh")]
        public decimal FromKwh { get; set; }

        [JsonPropertyName("toKwh")]
        public decimal ToKwh { get; set; }

        [JsonPropertyName("ratePerKwh")]
        public decimal RatePerKwh { get; set; }
    }
}
