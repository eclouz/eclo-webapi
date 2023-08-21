using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Validations.Products;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Products;

[Route("api/admin/products")]
[ApiController]
public class AdminProductsController : AdminBaseController
{
    private readonly IProductService _service;

    public AdminProductsController(IProductService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] ProductCreateDto dto)
    {
        var validator = new ProductCreateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateAsync(long productId, [FromForm] ProductUpdateDto dto)
    {
        var updateValidator = new ProductUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(productId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteAsync(long productId)
        => Ok(await _service.DeleteAsync(productId));
}
