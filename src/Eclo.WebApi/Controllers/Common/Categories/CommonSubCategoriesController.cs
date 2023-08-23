using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Categories;

[Route("api/common/subcategories")]
[ApiController]
public class CommonSubCategoriesController : CommonBaseController
{
    private readonly ISubCategoryService _service;
    private readonly int maxPageSize = 30;

    public CommonSubCategoriesController(ISubCategoryService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
      => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{subcategoryId}")]
    public async Task<IActionResult> GetByIdAsync(long subcategoryId)
    => Ok(await _service.GetByIdAsync(subcategoryId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());
}
