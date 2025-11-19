namespace AMI_projects.ViewModels
{
    public class ConsumerIndexViewModel
    {
        public List<ConsumerViewModel> Consumers { get; set; } = new List<ConsumerViewModel>();
        public ConsumerQueryViewModel Filters { get; set; } = new ConsumerQueryViewModel();
    }
}