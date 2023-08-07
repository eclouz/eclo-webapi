using Eclo.Application.Utilities;

namespace Eclo.DataAccess.Common;

public interface ISearchable<TModel>
{
    public Task<(int ItemsCount, IList<TModel>)> SearchAsync(string search, PaginationParams @params);
}
