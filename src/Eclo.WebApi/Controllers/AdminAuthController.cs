using Eclo.Persistence.Dtos.Auth;
using Eclo.Persistence.Validations;
using Eclo.Persistence.Validations.Auth;
using Eclo.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers;

[Route("api/admin/auth")]
[ApiController]
public class AdminAuthController : ControllerBase
{
    private readonly IAdminAuthService _authService;

    public AdminAuthController(IAdminAuthService authService)
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

    [HttpPost("reset/send-code")]
    public async Task<IActionResult> SendCodeResetPasswordAsync(string phone)
    {
        var result = PhoneNumberValidator.IsValid(phone);
        if (result == false) return BadRequest("Phone number is invalid!");
        var serviceResult = await _authService.SendCodeForResetPasswordAsync(phone);

        return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
    }

    [HttpPost("reset/verify")]
    public async Task<IActionResult> VerifyResetPasswordAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
    {
        var result = await _authService.VerifyResetPasswordAsync(verifyRegisterDto.PhoneNumber, verifyRegisterDto.Code);

        return Ok(new { result.Result, result.Token });
    }
}
