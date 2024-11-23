namespace Digital_Gravity.ViewModels
{
    public class PagedList<T> : List<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            this.AddRange(items);
        }

        public static PagedList<T> Create(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            return new PagedList<T>(items, totalCount, pageNumber, pageSize);
        }
    }
    }
