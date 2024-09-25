namespace CrossCutting.Utils;

public class PagedListResponse<T>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalContent { get; set; }
    public int TotalPages { get; set; }

    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public ICollection<T> Content { get; set; }

    public PagedListResponse(int currentPage, 
        int pageSize, 
        int totalPages, 
        int totalContent, 
        ICollection<T> pagedList)
    {
        PageNumber = currentPage;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalContent = totalContent;
        Content = pagedList;
    }
}