using Eclo.DataAccess.Common;
using Eclo.DataAccess.ViewModels.Orders;
using Eclo.Domain.Entities.Orders;

namespace Eclo.DataAccess.Interfaces.Orders;

public interface IOrderRepository : IRepository<Order, OrderViewModel>,
    IGetAll<OrderViewModel>
{
    public Task<long> CountOrderViewAsync();
}