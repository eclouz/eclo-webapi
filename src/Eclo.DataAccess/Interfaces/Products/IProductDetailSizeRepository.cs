using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Interfaces.Products;

public interface IProductDetailSizeRepository : IRepository<ProductDetailSize, ProductDetailSize>,
    IGetAll<ProductDetailSize>
{
    public Task<IList<ProductDetailSize>> GetAllByProductDetailIdAsync(long productDetailId);
}
