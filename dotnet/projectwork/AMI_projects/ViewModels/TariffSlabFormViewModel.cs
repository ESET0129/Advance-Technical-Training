using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AMI_projects.ViewModels
{
    public class TariffSlabFormViewModel
    {
        [JsonPropertyName("tariffId")]
        [Required]
        public int TariffId { get; set; }

        [JsonPropertyName("fromKwh")]
        [Required]
        public decimal FromKwh { get; set; }

        [JsonPropertyName("toKwh")]
        [Required]
        public decimal ToKwh { get; set; }

        [JsonPropertyName("ratePerKwh")]
        [Required]
        public decimal RatePerKwh { get; set; }
    }
}
