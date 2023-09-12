using Eclo.Persistence.Dtos.Auth;
using Eclo.Persistence.Validations.Auth;
using Eclo.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers;

[Route("api/head/auth")]
[ApiController]
public class HeadAuthController : ControllerBase
{
    private readonly IHeadAuthService _authService;

    public HeadAuthController(IHeadAuthService authService)
    {
        this._authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var validator = new LoginValidator();
        var valResult = validator.Validate(loginDto);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);
        var serviceResult = await _authService.LoginAsync(loginDto);

        return Ok(new { serviceResult.Result, serviceResult.Token });
    }
}
