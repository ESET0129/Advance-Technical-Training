namespace AMI_project.Dtos
{
    //Specific Query class for Meters:AMI_project.Api/Dtos/MeterQueryParameters.cs
    public class MeterQueryParameters : QueryParameters
    {
        public string? SerialNo { get; set; }
        public string? Status { get; set; }
        public DateTime? InstallDate { get; set; }
    }
}
