using CrossCutting.Utils;

namespace CrossCutting.Extensions;

public static class ListExtensions
{
    public static PagedListResponse<T> ToPaged<T>(this List<T> source, int pageNumber, int pageSize)
    {
        var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var pagedList = new PagedList<T>(items, source.Count, pageNumber, pageSize);

        return new PagedListResponse<T>(
            pagedList.CurrentPage,
            pagedList.PageSize,
            pagedList.TotalPages,
            pagedList.TotalContent,
            pagedList
        );
    }
}