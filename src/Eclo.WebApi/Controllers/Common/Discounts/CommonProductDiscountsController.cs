using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Discounts;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Discounts;

[Route("api/admin/product/discounts")]
[ApiController]
public class CommonProductDiscountsController : ControllerBase
{
    private readonly IProductDiscountService _service;
    private readonly int maxPageSize = 30;

    public CommonProductDiscountsController(IProductDiscountService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
      => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{discountId}")]
    public async Task<IActionResult> GetByIdAsync(long discountId)
    => Ok(await _service.GetByIdAsync(discountId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());
}
