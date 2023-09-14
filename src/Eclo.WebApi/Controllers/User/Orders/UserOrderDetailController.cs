using Eclo.Application.Utilities;
using Eclo.Persistence.Dtos.Orders;
using Eclo.Services.Interfaces.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.User.Orders;

[Route("api/user/order-detail")]
[ApiController]
public class UserOrderDetailController : UserBaseController
{
    private readonly IOrderDetailService _orderDetailService;
    private readonly int maxPageSize = 30;

    public UserOrderDetailController(IOrderDetailService orderDetailService)
    {
        this._orderDetailService = orderDetailService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _orderDetailService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{orderDetailId}")]
    public async Task<IActionResult> GetByIdAsync(long orderDetailId)
        => Ok(await _orderDetailService.GetByIdAsync(orderDetailId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _orderDetailService.CountAsync());

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] OrderDetailCreateDto orderDetailCreateDto)
        => Ok(await _orderDetailService.CreateAsync(orderDetailCreateDto));

    [HttpDelete("{orderDetailId}")]
    public async Task<IActionResult> DeleteAsync(long orderDetailId)
        => Ok(await _orderDetailService.DeleteAsync(orderDetailId));
}
