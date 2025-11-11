namespace AMI_projects.ViewModels
{
    public class MeterIndexViewModel
    {
        public List<MeterViewModel> Meters { get; set; } = new List<MeterViewModel>();
        public MeterQueryViewModel Filters { get; set; } = new MeterQueryViewModel();
        public PaginationInfoViewModel Pagination { get; set; } = new PaginationInfoViewModel();
    }
}
