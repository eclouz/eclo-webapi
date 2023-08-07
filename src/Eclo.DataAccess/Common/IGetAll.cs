using Eclo.Application.Utilities;

namespace Eclo.DataAccess.Common;

public interface IGetAll<TModel>
{
    public Task<IList<TModel>> GetAllAsync(PaginationParams @params);
}
