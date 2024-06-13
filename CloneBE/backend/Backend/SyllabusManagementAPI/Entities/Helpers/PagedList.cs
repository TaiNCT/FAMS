namespace SyllabusManagementAPI.Entities.Helpers
{
    public class PagedList<T> : List<T>
    {
        public Metadata Metadata { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Metadata = new Metadata()
            {
                TotalItems = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                // Example: 9 / 2 = 4.5 => 5
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            // If we need to get result at third page, counting 20 as the number of results we want.
            // That mean we need to skip ((3 - 1) * 20) = 40 results, and take the next 20 results.
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
