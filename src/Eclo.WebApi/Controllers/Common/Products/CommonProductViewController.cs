using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Products;

[Route("api/common/products/view")]
[ApiController]
public class CommonProductViewController : ControllerBase
{
    private readonly IProductService _service;
    private readonly int maxPageSize = 30;

    public CommonProductViewController(IProductService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
    => Ok(await _service.GetAllViewAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetByIdViewAsync(long productId)
        => Ok(await _service.GetByIdViewAsync(productId, new PaginationParams(1, maxPageSize)));
}
