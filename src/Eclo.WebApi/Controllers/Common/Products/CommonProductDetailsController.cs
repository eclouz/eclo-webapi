﻿using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Products;

[Route("api/common/product/details")]
[ApiController]
public class CommonProductDetailsController : CommonBaseController
{
    private readonly IProductDetailService _service;
    private readonly int maxPageSize = 30;

    public CommonProductDetailsController(IProductDetailService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
    => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{detailId}")]
    public async Task<IActionResult> GetByIdAsync(long detailId)
        => Ok(await _service.GetByIdAsync(detailId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync(string search, [FromQuery] int page = 1)
        => Ok(await _service.SearchAsync(search, new PaginationParams(page, maxPageSize)));

    [HttpGet("view/{id}")]
    public async Task<IActionResult> GetByIdViewAsync(long id)
        => Ok(await _service.GetByIdViewAsync(id, new PaginationParams(1, maxPageSize)));
}
