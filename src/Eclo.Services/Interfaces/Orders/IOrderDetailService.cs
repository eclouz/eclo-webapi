using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Orders;

namespace Eclo.Services.Interfaces.Orders;

public interface IOrderDetailService
{
    public Task<bool> DeleteAsync(long orderDetailId);

    public Task<long> CountAsync();

    public Task<IList<OrderDetail>> GetAllAsync(PaginationParams @params);

    public Task<OrderDetail> GetByIdAsync(long orderDetailId);
}