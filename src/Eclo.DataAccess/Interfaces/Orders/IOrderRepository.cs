using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Orders;

namespace Eclo.DataAccess.Interfaces.Orders;

public interface IOrderRepository : IRepository<Order, Order>,
    IGetAll<Order>, ISearchable<Order>
{
}