using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Categories;

namespace Eclo.DataAccess.Interfaces.Categories;

public interface ISubCategoryRepository : IRepository<SubCategory, SubCategory>
{
    public Task<IList<SubCategory>> GetAllAsync(long id, PaginationParams @params);
}