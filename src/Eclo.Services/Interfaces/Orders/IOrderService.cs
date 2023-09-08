using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Orders;

namespace Eclo.Services.Interfaces.Orders;

public interface IOrderService
{
    public Task<bool> DeleteAsync(long orderId);

    public Task<long> CountAsync();

    public Task<IList<Order>> GetAllAsync(PaginationParams @params);

    public Task<Order> GetByIdAsync(long orderId);
}