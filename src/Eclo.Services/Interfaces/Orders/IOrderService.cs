using Eclo.Application.Utilities;
using Eclo.DataAccess.ViewModels.Orders;
using Eclo.Persistence.Dtos.Orders;

namespace Eclo.Services.Interfaces.Orders;

public interface IOrderService
{
    public Task<bool> DeleteAsync(long orderId);

    public Task<bool> CreateAsync(OrderCreateDto orderCreateDto);

    public Task<long> CountAsync();

    public Task<IList<OrderViewModel>> GetAllAsync(PaginationParams @params);

    public Task<OrderViewModel> GetByIdAsync(long orderId);
}