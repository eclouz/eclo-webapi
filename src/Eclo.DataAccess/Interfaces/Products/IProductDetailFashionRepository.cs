using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Interfaces.Products;

public interface IProductDetailFashionRepository : IRepository<ProductDetailFashion, ProductDetailFashion>,
    IGetAll<ProductDetailFashion>
{
    public Task<ProductDetailFashion?> GetById(long id);
}
