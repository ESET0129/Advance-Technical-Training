using System.ComponentModel.DataAnnotations;
//DTO for creating a Tariff Slab
namespace AMI_project.Dtos
{
    public class TariffSlabCreateDto
    {
        [Required]
        public int TariffId { get; set; }

        [Required]
        public decimal FromKwh { get; set; }

        [Required]
        public decimal ToKwh { get; set; }

        [Required]
        public decimal RatePerKwh { get; set; }
    }
}
