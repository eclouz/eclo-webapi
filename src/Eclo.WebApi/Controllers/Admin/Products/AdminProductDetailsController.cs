using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Validations.Products;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Products;

[Route("api/admin/product/details")]
[ApiController]
public class AdminProductDetailsController : AdminBaseController
{
    private readonly IProductDetailService _service;

    public AdminProductDetailsController(IProductDetailService service)
    {
        this._service = service;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetAllAsync(long productId)
        => Ok(await _service.GetAllProductDetailsAsync(productId));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] ProductDetailCreateDto dto)
    {
        var validator = new ProductDetailCreateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{detailId}")]
    public async Task<IActionResult> UpdateAsync(long detailId, [FromForm] ProductDetailUpdateDto dto)
    {
        var updateValidator = new ProductDetailUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(detailId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{detailId}")]
    public async Task<IActionResult> DeleteAsync(long detailId)
        => Ok(await _service.DeleteAsync(detailId));
}
