using System.ComponentModel.DataAnnotations;

namespace AMI_projects.ViewModels
{
    public class ConsumerLoginViewModel
    {
        [Required]
        [Display(Name = "Consumer ID")]
        public long ConsumerId { get; set; }

        [Required]
        [Display(Name = "Consumer Name")]
        public string ConsumerName { get; set; }
    }
}
