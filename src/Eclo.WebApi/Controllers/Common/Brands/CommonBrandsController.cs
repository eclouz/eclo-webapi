using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Brands;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Brands;

[Route("api/common/brands")]
[ApiController]
public class CommonBrandsController : CommonBaseController
{
    private readonly IBrandService _service;
    private readonly int maxPageSize = 30;

    public CommonBrandsController(IBrandService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{brandId}")]
    public async Task<IActionResult> GetByIdAsync(long brandId)
        => Ok(await _service.GetByIdAsync(brandId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());
}
