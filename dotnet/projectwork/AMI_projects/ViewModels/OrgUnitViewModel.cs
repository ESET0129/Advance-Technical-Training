using System.Text.Json.Serialization;

namespace AMI_projects.ViewModels
{
    public class OrgUnitViewModel
    {
        [JsonPropertyName("orgUnitId")]
        public int OrgUnitId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("parentId")]
        public int? ParentId { get; set; }
    }
}
