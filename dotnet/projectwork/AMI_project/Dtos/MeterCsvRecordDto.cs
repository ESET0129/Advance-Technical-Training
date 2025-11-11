using CsvHelper.Configuration.Attributes;

namespace AMI_project.Dtos
{
    public class MeterCsvRecordDto
    {
        // We use CsvHelper attributes to map column names
        [Name("MeterSerialNo")]
        public string MeterSerialNo { get; set; }

        [Name("IpAddress")]
        public string IpAddress { get; set; }

        [Name("ICCID")]
        public string Iccid { get; set; }

        [Name("IMSI")]
        public string Imsi { get; set; }

        [Name("Manufacturer")]
        public string Manufacturer { get; set; }

        [Name("Firmware")]
        public string? Firmware { get; set; }

        [Name("Category")]
        public string Category { get; set; }

        [Name("InstallTsUtc")]
        public DateTime InstallTsUtc { get; set; }

        [Name("Status")]
        public string Status { get; set; }

        [Name("ConsumerId")]
        public long ConsumerId { get; set; }

        [Name("OrgUnitId")]
        public int OrgUnitId { get; set; }

        [Name("TariffId")]
        public int TariffId { get; set; }
    }
}
