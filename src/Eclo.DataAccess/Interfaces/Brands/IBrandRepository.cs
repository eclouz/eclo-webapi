using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Brands;

namespace Eclo.DataAccess.Interfaces.Brands;

public interface IBrandRepository : IRepository<Brand, Brand>,
    IGetAll<Brand>
{

}
