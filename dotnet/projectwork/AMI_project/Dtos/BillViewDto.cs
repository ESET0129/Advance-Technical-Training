namespace AMI_project.Dtos
{
    public class BillViewDto
    {
        public long BillId { get; set; }
        public string MeterSerialNo { get; set; }
        public DateOnly BillingMonth { get; set; }
        public decimal TotalKwh { get; set; }
        public decimal BaseRateApplied { get; set; }
        public decimal SlabCharge { get; set; }
        public decimal TotalBillAmount { get; set; }
        public string Status { get; set; }
        public DateOnly DueDate { get; set; }
    }
}
