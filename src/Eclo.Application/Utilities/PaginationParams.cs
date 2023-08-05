namespace Eclo.Application.Utilities;

public class PaginationParams
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int GetSkipCount()
    {
        return (PageNumber - 1) * PageSize;
    }

    public PaginationParams(int pageSize, int pageNumber)
    {
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
    }
}