namespace AMI_projects.ViewModels
{
    public class GenerateBillsResponseDto
    {
        public int SuccessfullyGenerated { get; set; }
        public int AlreadyExist { get; set; }
        public int NoReadingsFound { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
