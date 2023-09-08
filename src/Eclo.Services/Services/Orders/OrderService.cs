using Eclo.Application.Exceptions.Orders;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Orders;
using Eclo.DataAccess.ViewModels.Orders;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Orders;

namespace Eclo.Services.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaginator _paginator;

    public OrderService(IOrderRepository orderRepository,
        IPaginator paginator)
    {
        this._orderRepository = orderRepository;
        this._paginator = paginator;
    }
    public async Task<long> CountAsync() => await _orderRepository.CountAsync();

    public async Task<bool> DeleteAsync(long orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null) throw new OrderNotFoundException();

        var dbResult = await _orderRepository.DeleteAsync(orderId);

        return dbResult > 0;
    }

    public async Task<IList<OrderViewModel>> GetAllAsync(PaginationParams @params)
    {
        var orders = await _orderRepository.GetAllAsync(@params);
        var count = await _orderRepository.CountAsync();
        _paginator.Paginate(count, @params);

        return orders;
    }

    public async Task<OrderViewModel> GetByIdAsync(long orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null) throw new OrderNotFoundException();
        else return order;
    }
}