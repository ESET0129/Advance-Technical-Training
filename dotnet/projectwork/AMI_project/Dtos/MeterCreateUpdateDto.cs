using System.ComponentModel.DataAnnotations;
//DTO for creating/updating a Meter:AMI_project.Api/Dtos/MeterCreateUpdateDto.cs
namespace AMI_project.Dtos
{
    public class MeterCreateUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string MeterSerialNo { get; set; }

        [Required]
        [StringLength(45)]
        public string IpAddress { get; set; }

        [Required]
        [StringLength(30)]
        public string Iccid { get; set; }

        [Required]
        [StringLength(30)]
        public string Imsi { get; set; }

        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; }

        [StringLength(50)]
        public string? Firmware { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        public DateTime InstallTsUtc { get; set; }

        [Required]
        public string Status { get; set; } // "Active", "Inactive", "Decommissioned"

        // Foreign Keys
        [Required]
        public long ConsumerId { get; set; }
        [Required]
        public int OrgUnitId { get; set; }
        [Required]
        public int TariffId { get; set; }
    }
}
