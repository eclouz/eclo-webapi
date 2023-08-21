using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Products;

[Route("api/common/product/comments")]
[ApiController]
public class CommonProductCommentsController : CommonBaseController
{
    private readonly IProductCommentService _service;
    private readonly int maxPageSize = 30;

    public CommonProductCommentsController(IProductCommentService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{productCommentId}")]
    public async Task<IActionResult> GetByIdAsync(long productCommentId)
        => Ok(await _service.GetByIdAsync(productCommentId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());
}
