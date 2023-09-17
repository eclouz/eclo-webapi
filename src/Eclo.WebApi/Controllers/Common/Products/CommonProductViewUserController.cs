using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Products;

[Route("api/common/products/view/user")]
[ApiController]
public class CommonProductViewUserController : ControllerBase
{
    private readonly IProductService _service;
    private readonly int maxPageSize = 30;

    public CommonProductViewUserController(IProductService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(long userId, [FromQuery] int page = 1)
    => Ok(await _service.GetAllUserIdViewAsync(userId, new PaginationParams(page, maxPageSize)));

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetByIdViewAsync(long userId, long productId)
        => Ok(await _service.GetByIdUserViewAsync(userId, productId, new PaginationParams(1, maxPageSize)));
}
