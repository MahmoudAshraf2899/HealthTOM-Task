namespace Boilerplate.Application.Helpers;

public class PaginatedResponse<T>
{
    public int? PageNumber { get; }
    public int? PageSize { get; }
    public int? TotalPages { get; }
    public int? TotalCount { get; }

    public int? StartPage { get; }
    public int? EndPage { get; }
    public IEnumerable<T> Items { get; }

    public PaginatedResponse(IEnumerable<T> items, int? count,  int? totalPages,int? startPage,int? endPage)
    {
        TotalCount = count;
        TotalPages = totalPages;
        Items = items;
        StartPage = startPage;
        EndPage = endPage;
    }

    public static PaginatedResponse<T> Response(IEnumerable<T> items, int? count,  int? totalPages,int? startPage,int? endPage)
    {
        return new PaginatedResponse<T>(items,count,totalPages,startPage,endPage);
    }
}