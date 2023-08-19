using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Discounts;

namespace Eclo.DataAccess.Interfaces.Products;

public interface IProductDiscountRepository : IRepository<ProductDiscount, ProductDiscount>,
    IGetAll<ProductDiscount>
{
}
