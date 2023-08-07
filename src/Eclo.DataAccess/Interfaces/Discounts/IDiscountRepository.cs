using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Discounts;

namespace Eclo.DataAccess.Interfaces.Discounts;

public interface IDiscountRepository : IRepository<Discount, Discount>,
    IGetAll<Discount>
{

}