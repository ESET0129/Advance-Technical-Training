namespace AMI_project.Dtos
{
    public class BillQueryParameters : QueryParameters
    {
        public string? MeterSerialNo { get; set; }
        public string? Status { get; set; }
        public DateOnly? BillingMonth { get; set; }
    }
}
