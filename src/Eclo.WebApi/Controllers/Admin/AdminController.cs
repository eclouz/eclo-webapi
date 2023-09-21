using Eclo.Persistence.DTOs.Admins;
using Eclo.Persistence.Validations.Admins;
using Eclo.Services.Interfaces.Admins;
using Eclo.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin;

[Route("api/admin/profile")]
[ApiController]
public class AdminController : AdminBaseController
{
    private readonly IAdminService _adminService;
    private readonly IIdentityService _identityService;

    public AdminController(IAdminService adminService,
        IIdentityService identityService)
    {
        this._adminService = adminService;
        this._identityService = identityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync()
        => Ok(await _adminService.GetByIdAsync());

    [HttpPut("adminId")]
    public async Task<IActionResult> UpdateAsync([FromForm] AdminUpdateDto dto)
    {
        var updateValidator = new AdminUpdateValidator();
        var validationResult = await updateValidator.ValidateAsync(dto);
        if (validationResult.IsValid) return Ok(await _adminService.UpdateAsync(_identityService.Id, dto));
        else return BadRequest(validationResult.Errors);
    }
}
