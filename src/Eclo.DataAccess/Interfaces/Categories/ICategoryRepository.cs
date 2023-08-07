using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Categories;

namespace Eclo.DataAccess.Interfaces.Categories;

public interface ICategoryRepository : IRepository<Category, Category>,
    IGetAll<Category>
{

}
