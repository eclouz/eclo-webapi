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

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] AdminCreateDto dto)
    {
        var validator = new AdminCreateValidator();
        var validatorresult = await validator.ValidateAsync(dto);
        if (validatorresult.IsValid) return Ok(await _adminService.CreateAsync(dto));
        else return BadRequest(validatorresult.Errors);
    }
}
