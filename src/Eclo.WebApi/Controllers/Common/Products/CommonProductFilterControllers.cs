﻿using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Products;

[Route("api/common/products/filter")]
[ApiController]
public class CommonProductFilterControllers : CommonBaseController
{
    private readonly IProductService _service;
    private readonly int maxPageSize = 30;

    public CommonProductFilterControllers(IProductService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFilterAsync([FromQuery] string category, [FromQuery] int min, [FromQuery] int max, [FromQuery] List<string> subCategories, [FromQuery] int page = 1)
    => Ok(await _service.FiltrAsync(category, min, max, subCategories, new PaginationParams(page, maxPageSize)));


    [HttpGet("category")]
    public async Task<IActionResult> GetAlluserIdFilterAsync([FromQuery] long userId, [FromQuery] string categoryName, [FromQuery] int min, [FromQuery] int max, [FromQuery] List<string> subCategoriesName, [FromQuery] int page = 1)
    => Ok(await _service.FiltrUserIdAsync(userId, categoryName, min, max, subCategoriesName, new PaginationParams(page, maxPageSize)));
}
