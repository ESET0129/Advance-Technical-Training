using System.Text.Json.Serialization;

namespace AMI_projects.ViewModels
{
    public class MeterFormViewModel
    {
        [JsonPropertyName("meterSerialNo")]
        public string MeterSerialNo { get; set; }

        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }

        [JsonPropertyName("iccid")]
        public string Iccid { get; set; }

        [JsonPropertyName("imsi")]
        public string Imsi { get; set; }

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonPropertyName("firmware")]
        public string? Firmware { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("installTsUtc")]
        public DateTime InstallTsUtc { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("consumerId")]
        public long? ConsumerId { get; set; }

        [JsonPropertyName("orgUnitId")]
        public int OrgUnitId { get; set; }

        [JsonPropertyName("tariffId")]
        public int TariffId { get; set; }
    }
}
