using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Validations.Products;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Products;

[Route("api/admin/product/detail/sizes")]
[ApiController]
public class AdminProductDetailSizesController : ControllerBase
{
    private readonly IProductDetailSizeService _service;

    public AdminProductDetailSizesController(IProductDetailSizeService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] ProductDetailSizeCreateDto dto)
    {
        var validator = new ProductDetailSizeCreateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{sizeId}")]
    public async Task<IActionResult> UpdateAsync(long sizeId, [FromForm] ProductDetailSizeUpdateDto dto)
    {
        var updateValidator = new ProductDetailSizeUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(sizeId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{sizeId}")]
    public async Task<IActionResult> DeleteAsync(long sizeId)
       => Ok(await _service.DeleteAsync(sizeId));
}
