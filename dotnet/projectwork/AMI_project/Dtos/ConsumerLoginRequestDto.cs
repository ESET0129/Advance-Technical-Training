using System.ComponentModel.DataAnnotations;

namespace AMI_project.Dtos
{
    public class ConsumerLoginRequestDto
    {
        [Required]
        public long ConsumerId { get; set; }

        [Required]
        public string ConsumerName { get; set; }
    }
}
