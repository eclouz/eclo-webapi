using Eclo.Application.Utilities;
using Eclo.DataAccess.ViewModels.Orders;

namespace Eclo.Services.Interfaces.Orders;

public interface IOrderDetailService
{
    public Task<bool> DeleteAsync(long orderDetailId);

    public Task<long> CountAsync();

    public Task<IList<OrderViewModel>> GetAllAsync(PaginationParams @params);

    public Task<OrderViewModel> GetByIdAsync(long orderDetailId);
}