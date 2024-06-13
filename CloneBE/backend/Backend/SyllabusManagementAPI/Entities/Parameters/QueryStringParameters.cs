namespace SyllabusManagementAPI.Entities.Parameters
{
    /// <summary>
    /// Represents the base class for query string parameters used in API requests.
    /// </summary>
    public abstract class QueryStringParameters
    {
        const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            // If the requested page size is greater than the max page size, return the max page size
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
