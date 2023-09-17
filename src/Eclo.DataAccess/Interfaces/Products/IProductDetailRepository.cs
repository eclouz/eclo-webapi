using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Interfaces.Products;

public interface IProductDetailRepository : IRepository<ProductDetail, ProductDetail>,
    IGetAll<ProductDetail>, ISearchable<ProductDetail>
{
    public Task<ProductDetail?> GetById(long id);
}
