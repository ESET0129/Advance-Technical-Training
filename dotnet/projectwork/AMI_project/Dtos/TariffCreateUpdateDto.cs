using System.ComponentModel.DataAnnotations;

namespace AMI_project.Dtos
{
    public class TariffCreateUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        [Required]
        public decimal BaseRate { get; set; }

        // We'll keep TaxRate here as the DB has it,
        // but your app logic can ignore it
        public decimal TaxRate { get; set; } = 0;
    }
}
