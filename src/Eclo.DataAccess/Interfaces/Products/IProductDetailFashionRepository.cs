using Eclo.DataAccess.Common;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Interfaces.Products;

public interface IProductDetailFashionRepository : IRepository<ProductDetailFashion, ProductDetailFashionViewModel>,
    IGetAll<ProductDetailFashionViewModel>
{
}
