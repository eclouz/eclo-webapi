using Eclo.DataAccess.Common;
using Eclo.DataAccess.ViewModels.Orders;
using Eclo.Domain.Entities.Orders;

namespace Eclo.DataAccess.Interfaces.Orders;

public interface IOrderDetailRepository : IRepository<OrderDetail, OrderViewModel>,
    IGetAll<OrderViewModel>, ISearchable<OrderViewModel>
{
}