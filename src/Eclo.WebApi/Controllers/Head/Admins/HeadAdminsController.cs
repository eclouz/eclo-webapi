using Eclo.Application.Utilities;
using Eclo.Persistence.DTOs.Admins;
using Eclo.Persistence.Validations.Admins;
using Eclo.Services.Interfaces.Admins;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Head.Admins;

[Route("api/head/admins")]
[ApiController]
public class HeadAdminsController : HeadBaseController
{
    private readonly IAdminService _adminService;
    private readonly int maxPageSize = 30;

    public HeadAdminsController(IAdminService adminService)
    {
        this._adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _adminService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _adminService.CountAsync());

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync(string search, [FromQuery] int page = 1)
        => Ok(await _adminService.SearchAsync(search, new PaginationParams(page, maxPageSize)));

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] AdminCreateDto dto)
    {
        var validator = new AdminCreateValidator();
        var validatorresult = await validator.ValidateAsync(dto);
        if (validatorresult.IsValid) return Ok(await _adminService.CreateAsync(dto));
        else return BadRequest(validatorresult.Errors);
    }

    [HttpDelete("{adminId}")]
    public async Task<IActionResult> DeleteAsync(long adminId)
        => Ok(await _adminService.DeleteAsync(adminId));

    [HttpPut("{adminId}")]
    public async Task<IActionResult> UpdateAsync(long adminId, [FromForm] AdminUpdateDto dto)
    {
        var updateValidator = new AdminUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _adminService.UpdateAsync(adminId, dto));
        else return BadRequest(validationResult.Errors);
    }
}
