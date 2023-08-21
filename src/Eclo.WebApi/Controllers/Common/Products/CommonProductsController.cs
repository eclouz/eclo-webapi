using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Products;

[Route("api/common/products")]
[ApiController]
public class CommonProductsController : CommonBaseController
{
    private readonly IProductService _service;
    private readonly int maxPageSize = 30;

    public CommonProductsController(IProductService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
    => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetByIdAsync(long productId)
        => Ok(await _service.GetByIdAsync(productId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync(string search, [FromQuery] int page = 1)
        => Ok(await _service.SearchAsync(search, new PaginationParams(page, maxPageSize)));
}
