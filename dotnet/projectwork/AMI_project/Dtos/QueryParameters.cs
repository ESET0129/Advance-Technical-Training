namespace AMI_project.Dtos
{
    // This class will be reused by all controllers for pagination and sorting
    public class QueryParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string SortBy { get; set; } = "MeterSerialNo"; // Default sort
        public string SortOrder { get; set; } = "ASC"; // ASC or DESC
    }
}
