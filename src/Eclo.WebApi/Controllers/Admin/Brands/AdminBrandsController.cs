using Eclo.Persistence.Dtos.Brands;
using Eclo.Persistence.Validations.Brands;
using Eclo.Services.Interfaces.Brands;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Brands;

[Route("api/admin/brands")]
[ApiController]
public class AdminBrandsController : AdminBaseController
{
    private readonly IBrandService _service;

    public AdminBrandsController(IBrandService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] BrandCreateDto dto)
    {
        var validator = new BrandCreateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreatAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{brandId}")]
    public async Task<IActionResult> UpdateAsync(long brandId, [FromForm] BrandUpdateDto dto)
    {
        var updateValidator = new BrandUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(brandId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{brandId}")]
    public async Task<IActionResult> DeleteAsync(long brandId)
        => Ok(await _service.DeleteAsync(brandId));
}
