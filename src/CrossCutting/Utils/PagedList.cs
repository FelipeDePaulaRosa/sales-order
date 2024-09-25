namespace CrossCutting.Utils;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalContent { get; private set; }
    
    public PagedList(IEnumerable<T> items, int totalContent, int pageNumber, int pageSize)
    {
        TotalContent = totalContent;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalContent / (double)pageSize);
        AddRange(items);
    }
}