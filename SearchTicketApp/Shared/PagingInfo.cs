using System.Text.Json.Serialization;

namespace SearchTicketApp.Shared
{
    public class PagingInfo<T>
    {
        [JsonConstructor]
        public PagingInfo()
        {
            
        }

        public static PagingInfo<T> Create(ICollection<T> items, int totalCount, int page, int pageSize)
        {
            return new PagingInfo<T>()
            {
                Items = items,
                PageSize = pageSize,
                TotalCount = totalCount,
                Page = page,
                TotalPages = (int)MathF.Ceiling((float)totalCount / pageSize),
            };
        }

        public ICollection<T> Items { get; private set; } = default!;

        public int PageSize { get; private set; }

        public int Page { get; private set; }

        public int TotalPages { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasNext => Page < TotalPages;

        public bool HasPrevious => Page > 1;
    }
}
