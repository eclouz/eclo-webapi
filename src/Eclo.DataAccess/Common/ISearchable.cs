using Eclo.Application.Utilities;

namespace Eclo.DataAccess.Common;

public interface ISearchable<TModel>
{
    public Task<(long ItemsCount, IList<TModel>)> SearchAsync(string search, PaginationParams @params);
}
