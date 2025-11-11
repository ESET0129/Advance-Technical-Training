namespace AMI_projects.ViewModels
{
    public class MonthlyBillViewModel
    {
        public long BillId { get; set; }
        public string MeterSerialNo { get; set; }
        public DateOnly BillingMonth { get; set; }
        public decimal TotalKwh { get; set; }
        public decimal TotalBillAmount { get; set; }
        public DateOnly DueDate { get; set; }
        public string Status { get; set; }
    }
}
