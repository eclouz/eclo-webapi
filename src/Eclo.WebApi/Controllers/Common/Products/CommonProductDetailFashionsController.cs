using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Products;

[Route("api/common/product/detail/fashions")]
[ApiController]
public class CommonProductDetailFashionsController : CommonBaseController
{
    private readonly IProductDetailFashionService _service;
    private readonly int maxPageSize = 30;

    public CommonProductDetailFashionsController(IProductDetailFashionService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
    => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{fashionId}")]
    public async Task<IActionResult> GetByIdAsync(long fashionId)
        => Ok(await _service.GetByIdAsync(fashionId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());
}
