using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Validations.Products;
using Eclo.Services.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Products;

[Route("api/product/detail/fashions")]
[ApiController]
public class AdminProductDetailFashionsController : AdminBaseController
{
    private readonly IProductDetailFashionService _service;

    public AdminProductDetailFashionsController(IProductDetailFashionService service)
    {
        this._service = service;
    }

    [HttpGet("{productDetailId}")]
    public async Task<IActionResult> GetAllFashionsAsync(long productDetailId)
        => Ok(await _service.GetAllFashionsAsync(productDetailId));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] ProductDetailFashionCreateDto dto)
    {
        var validator = new ProductDetailFashionCreateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{fashionId}")]
    public async Task<IActionResult> UpdateAsync(long fashionId, [FromForm] ProductDetailFashionUpdateDto dto)
    {
        var updateValidator = new ProductDetailFashionUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(fashionId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{fashionId}")]
    public async Task<IActionResult> DeleteAsync(long fashionId)
        => Ok(await _service.DeleteAsync(fashionId));
}