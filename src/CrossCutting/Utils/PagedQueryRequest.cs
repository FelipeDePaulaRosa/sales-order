namespace CrossCutting.Utils;

public abstract class PagedQueryRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public PagedQueryRequest(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}