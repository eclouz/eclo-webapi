using Eclo.Application.Exceptions.Orders;
using Eclo.Application.LogicServices;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Discounts;
using Eclo.DataAccess.Interfaces.Orders;
using Eclo.DataAccess.ViewModels.Orders;
using Eclo.Domain.Entities.Orders;
using Eclo.Persistence.Dtos.Orders;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Orders;

namespace Eclo.Services.Services.Orders;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IPaginator _paginator;

    public OrderDetailService(IOrderDetailRepository orderDetailRepository,
        IPaginator paginator,
        IDiscountRepository discountRepository)
    {
        this._orderDetailRepository = orderDetailRepository;
        this._discountRepository = discountRepository;
        this._paginator = paginator;
    }
    public async Task<long> CountAsync() => await _orderDetailRepository.CountAsync();

    public async Task<bool> CreateAsync(OrderDetailCreateDto orderDetailCreateDto)
    {
        var discounts = await _discountRepository.GetByIdAsync(orderDetailCreateDto.ProductDiscountId);

        OrderDetail orderDetail = new OrderDetail()
        {
            OrderId = orderDetailCreateDto.OrderId,
            ProductDiscountId = orderDetailCreateDto.ProductDiscountId,
            Quantity = orderDetailCreateDto.Quantity,
            Price = orderDetailCreateDto.Price,
            DiscountPrice = GetTotalPrice.DiscountPrice(discounts!.Percentage, orderDetailCreateDto.Price),
            TotalPrice = GetTotalPrice.TotalPrice(discounts!.Percentage, orderDetailCreateDto.Price, orderDetailCreateDto.Quantity),
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _orderDetailRepository.CreateAsync(orderDetail);

        return result > 0;
    }

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
        var count = await _orderDetailRepository.CountOrderViewAsync();
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