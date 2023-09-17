using Eclo.Persistence.Dtos.Discounts;
using Eclo.Persistence.Validations.Discounts;
using Eclo.Services.Interfaces.Discounts;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Discounts;

[Route("api/admin/product/discounts")]
[ApiController]
public class AdminProductDiscountsController : ControllerBase
{
    private readonly IProductDiscountService _service;

    public AdminProductDiscountsController(IProductDiscountService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] ProductDiscountCreateDto dto)
    {
        var validator = new ProductDiscountCreateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{dicountId}")]
    public async Task<IActionResult> UpdateAsync(long dicountId, [FromForm] ProductDiscountUpdateDto dto)
    {
        var updateValidator = new ProductDiscountUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(dicountId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{dicountId}")]
    public async Task<IActionResult> DeleteAsync(long dicountId)
       => Ok(await _service.DeleteAsync(dicountId));
}
