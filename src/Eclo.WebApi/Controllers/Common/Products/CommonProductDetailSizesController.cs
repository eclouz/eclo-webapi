using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Products;

[Route("api/common/product/detail/sizes")]
[ApiController]
public class CommonProductDetailSizesController : ControllerBase
{
    private readonly IProductDetailSizeService _service;
    private readonly int maxPageSize = 30;

    public CommonProductDetailSizesController(IProductDetailSizeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
      => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{sizeId}")]
    public async Task<IActionResult> GetByIdAsync(long sizeId)
    => Ok(await _service.GetByIdAsync(sizeId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());
}
