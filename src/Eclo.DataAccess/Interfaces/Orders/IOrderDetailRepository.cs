using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Orders;

namespace Eclo.DataAccess.Interfaces.Orders;

public interface IOrderDetailRepository : IRepository<OrderDetail, OrderDetail>,
    IGetAll<OrderDetail>, ISearchable<OrderDetail>
{
}