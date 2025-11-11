//DTO for creating/updating a Consumer
using System.ComponentModel.DataAnnotations;

namespace AMI_project.Dtos
{
    public class ConsumerCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(30)]
        public string? Phone { get; set; }

        [StringLength(200)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // "Active" or "Inactive"

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; } // Username

        // UpdatedBy is optional, can be set by the repository
        //[StringLength(50)]
        //public string? UpdatedBy { get; set; }
    }
}
