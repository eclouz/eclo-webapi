using Eclo.Application.Utilities;
using Eclo.DataAccess.ViewModels.Orders;
using Eclo.Domain.Entities.Orders;

namespace Eclo.Services.Interfaces.Orders;

public interface IOrderService
{
    public Task<bool> DeleteAsync(long orderId);

    public Task<long> CountAsync();

    public Task<IList<OrderViewModel>> GetAllAsync(PaginationParams @params);

    public Task<OrderViewModel> GetByIdAsync(long orderId);
}