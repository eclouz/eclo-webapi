using Eclo.DataAccess.Common;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Interfaces.Products;

public interface IProductDetailRepository : IRepository<ProductDetail, ProductViewModel>,
    IGetAll<ProductViewModel>, ISearchable<ProductViewModel>
{
    public Task<ProductDetail?> GetById(long id);
}
