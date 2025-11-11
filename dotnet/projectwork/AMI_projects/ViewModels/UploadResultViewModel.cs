using System.Text.Json.Serialization;

namespace AMI_projects.ViewModels
{
    public class UploadResultViewModel
    {
        [JsonPropertyName("totalRows")]
        public int TotalRows { get; set; }

        [JsonPropertyName("successfullyImported")]
        public int SuccessfullyImported { get; set; }

        [JsonPropertyName("failedRows")]
        public int FailedRows { get; set; }

        [JsonPropertyName("errorMessages")]
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
