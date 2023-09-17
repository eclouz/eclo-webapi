using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Categories;

namespace Eclo.DataAccess.Interfaces.Categories;

public interface ISubCategoryRepository : IRepository<SubCategory, SubCategory>,
    IGetAll<SubCategory>
{

}