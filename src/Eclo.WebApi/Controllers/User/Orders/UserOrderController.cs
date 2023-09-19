using Eclo.Application.Utilities;
using Eclo.Persistence.Dtos.Orders;
using Eclo.Services.Interfaces.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.User.Orders;

[Route("api/user/order")]
[ApiController]
public class UserOrderController : UserBaseController
{
    private readonly IOrderService _orderService;
    private readonly int maxPageSize = 30;

    public UserOrderController(IOrderService orderService)
    {
        this._orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _orderService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetByIdAsync(long orderId)
        => Ok(await _orderService.GetByIdAsync(orderId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _orderService.CountOrderViewAsync());

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] OrderCreateDto orderCreateDto)
        => Ok(await _orderService.CreateAsync(orderCreateDto));

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteAsync(long orderId)
        => Ok(await _orderService.DeleteAsync(orderId));
}
