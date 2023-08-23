using Eclo.Application.Utilities;
using Eclo.DataAccess.Common;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Products;

namespace Eclo.DataAccess.Interfaces.Products;

public interface IProductRepository : IRepository<Product, Product>,
    IGetAll<Product>, ISearchable<Product>
{
    public Task<Product?> GetById(long id);

    public Task<IList<ProductGetViewModel>> GetAllView(PaginationParams @params);
}
