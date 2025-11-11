namespace AMI_project.Dtos
{
    public class UploadResultDto
    {
        public int TotalRows { get; set; }
        public int SuccessfullyImported { get; set; }
        public int FailedRows { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
