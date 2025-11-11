namespace SearchTicketApp.Shared
{
    public class PagingInfo<T>
    {
        public ICollection<T> Items { get; set; } = default!;

        public int PageSize { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public int PageCount { get; set; }

        public bool HasNext => Page < TotalPages;

        public bool HasPrevious => Page > 1;
    }
}
