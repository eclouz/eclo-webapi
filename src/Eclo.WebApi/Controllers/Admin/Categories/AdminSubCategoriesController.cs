using Eclo.Persistence.Dtos.Categories;
using Eclo.Persistence.Validations.Categories;
using Eclo.Services.Interfaces.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Categories;

[Route("api/admin/subcategories")]
[ApiController]
public class AdminSubCategoriesController : AdminBaseController
{
    private readonly ISubCategoryService _service;

    public AdminSubCategoriesController(ISubCategoryService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] SubCategoryCreateDto dto)
    {
        var validator = new SubCategoryCreateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("{subcategoryId}")]
    public async Task<IActionResult> UpdateAsync(long subcategoryId, [FromForm] SubCategoryUpdateDto dto)
    {
        var updateValidator = new SubCategoryUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(subcategoryId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete("{subcategoryId}")]
    public async Task<IActionResult> DeleteAsync(long subcategoryId)
       => Ok(await _service.DeleteAsync(subcategoryId));
}
