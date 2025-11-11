//DTO for creating a User
using System.ComponentModel.DataAnnotations;

namespace AMI_project.Dtos
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; } // We will hash this

        public bool IsActive { get; set; } = true;
    }
}
