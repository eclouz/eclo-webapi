using Eclo.Persistence.Dtos.Discounts;
using Eclo.Persistence.Validations.Discounts;
using Eclo.Services.Interfaces.Discounts;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Discounts;

[Route("api/admin/discounts")]
[ApiController]
public class AdminDiscountsController : ControllerBase
{
    private readonly IDiscountService _service;

    public AdminDiscountsController(IDiscountService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] DiscountCreateDto dto)
    {
        var validator = new DiscountCreateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{discountId}")]
    public async Task<IActionResult> UpdateAsync(long discountId, [FromForm] DiscountUpdateDto dto)
    {
        var updateValidator = new DiscountUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(discountId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{discountId}")]
    public async Task<IActionResult> DeleteAsync(long discountId)
       => Ok(await _service.DeleteAsync(discountId));
}
