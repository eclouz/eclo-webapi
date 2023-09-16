using Eclo.Persistence.Dtos.Auth;
using Eclo.Persistence.Validations;
using Eclo.Persistence.Validations.Auth;
using Eclo.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers;

[Route("api/user/auth")]
[ApiController]
public class UserAuthController : ControllerBase
{
    private readonly IUserAuthService _authService;

    public UserAuthController(IUserAuthService authService)
    {
        this._authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto registerDto)
    {
        var validator = new RegisterValidator();
        var result = validator.Validate(registerDto);
        if (result.IsValid)
        {
            var serviceResult = await _authService.RegisterAsync(registerDto);
            return Ok(new { serviceResult.Result, serviceResult.CachedMinutes });
        }
        else return BadRequest(result.Errors);
    }

    [HttpPost("register/send-code")]
    public async Task<IActionResult> SendCodeRegisterAsync(string phone)
    {
        var result = PhoneNumberValidator.IsValid(phone);
        if (result == false) return BadRequest("Phone number is invalid!");

        var serviceResult = await _authService.SendCodeForRegisterAsync(phone);
        return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
    }

    [HttpPost("register/verify")]
    public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
    {
        var serviceResult = await _authService.VerifyRegisterAsync(verifyRegisterDto.PhoneNumber, verifyRegisterDto.Code);
        return Ok(new { serviceResult.Result, serviceResult.Token });
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
    public async Task<IActionResult> SentCodeResetPasswordAsync(string phone)
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

    [HttpPut("reset/update-password")]
    public async Task<IActionResult> UpdatePasswordAsync([FromForm] ResetPasswordDto resetPasswordDto)
    {
        var validator = new ResetPasswordValidator();
        var validatorResult = validator.Validate(resetPasswordDto);
        if (validatorResult.IsValid)
        {
            var result =  await _authService.UpdatePasswordAsync(resetPasswordDto);
            return Ok(new { result.Result, result.CachedMinutes });
        }
        else return BadRequest(validatorResult.Errors);
    }
}
