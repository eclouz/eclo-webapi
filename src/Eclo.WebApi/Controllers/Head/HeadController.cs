using Eclo.Persistence.DTOs.Heads;
using Eclo.Persistence.Validations.Heads;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Heads;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Head;

[Route("api/head/profile")]
[ApiController]
public class HeadController : HeadBaseController
{
    private readonly IHeadService _headService;
    private readonly IIdentityService _identityService;

    public HeadController(IHeadService headService,
        IIdentityService identityService)
    {
        this._headService = headService;
        this._identityService = identityService;
    }

    [HttpPut("{headId}")]
    public async Task<IActionResult> UpdateAsync([FromForm] HeadUpdateDto dto)
    {
        var updateValidator = new HeadUpdateValidator();
        var validationResult = await updateValidator.ValidateAsync(dto);
        if (validationResult.IsValid) return Ok(await _headService.UpdateAsync(_identityService.Id, _identityService.PhoneNumber, dto));
        else return BadRequest(validationResult.Errors);
    }
}
