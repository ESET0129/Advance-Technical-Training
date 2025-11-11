namespace AMI_projects.ViewModels
{
    public class MeterQueryViewModel
    {
        public string? SerialNo { get; set; }
        public string? Status { get; set; }
        public DateOnly? InstallDate { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}
