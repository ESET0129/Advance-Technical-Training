//DTO for updating a User
using System.ComponentModel.DataAnnotations;

namespace AMI_project.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        // Optional: If new password is provided, we update it
        [MinLength(8)]
        public string? Password { get; set; }

        public bool IsActive { get; set; }
    }
}
