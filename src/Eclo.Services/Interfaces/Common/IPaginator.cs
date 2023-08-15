using Eclo.Application.Utilities;

namespace Eclo.Services.Interfaces.Common;

public interface IPaginator
{
    public void Paginate(long ItemsCount, PaginationParams @params);
}
