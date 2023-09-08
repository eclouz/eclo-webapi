using Eclo.Application.Exceptions.Orders;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Orders;
using Eclo.DataAccess.ViewModels.Orders;
using Eclo.Domain.Entities.Orders;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Orders;

namespace Eclo.Services.Services.Orders;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IPaginator _paginator;

    public OrderDetailService(IOrderDetailRepository orderDetailRepository,
        IPaginator paginator)
    {
        this._orderDetailRepository = orderDetailRepository;
        this._paginator = paginator;
    }
    public async Task<long> CountAsync() => await _orderDetailRepository.CountAsync();

    public async Task<bool> DeleteAsync(long orderDetailId)
    {
        var order = await _orderDetailRepository.GetByIdAsync(orderDetailId);
        if (order == null) throw new OrderNotFoundException();

        var dbResult = await _orderDetailRepository.DeleteAsync(orderDetailId);

        return dbResult > 0;
    }

    public async Task<IList<OrderViewModel>> GetAllAsync(PaginationParams @params)
    {
        var orders = await _orderDetailRepository.GetAllAsync(@params);
        var count = await _orderDetailRepository.CountAsync();
        _paginator.Paginate(count, @params);

        return orders;
    }

    public async Task<OrderViewModel> GetByIdAsync(long orderDetailId)
    {
        var order = await _orderDetailRepository.GetByIdAsync(orderDetailId);
        if (order == null) throw new OrderNotFoundException();
        else return order;
    }
}