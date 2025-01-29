namespace Store.Domain.Responses;

public class Pager<T>
{
    public int TotalItems { get; }
    public int PageSize { get; }
    public int CurrentPage { get; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public IEnumerable<T> Items { get; }

    public Pager(IEnumerable<T> items, int totalItems, int pageSize, int currentPage)
    {
        Items = items;
        TotalItems = totalItems;
        PageSize = pageSize;
        CurrentPage = currentPage;
    }
}