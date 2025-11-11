using System.ComponentModel.DataAnnotations;

namespace AMI_project.Dtos
{
    public class ConsumerUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string Status { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
