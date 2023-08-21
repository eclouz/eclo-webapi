using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Products;

[Route("api/admin/user/product/likes")]
[ApiController]
public class CommonUserProductLikesController : ControllerBase
{
    private readonly IUserProductLikeService _service;
    private readonly int maxPageSize = 30;

    public CommonUserProductLikesController(IUserProductLikeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
      => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{likeId}")]
    public async Task<IActionResult> GetByIdAsync(long likeId)
    => Ok(await _service.GetByIdAsync(likeId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());
}
