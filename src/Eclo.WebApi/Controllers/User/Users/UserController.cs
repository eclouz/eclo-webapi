using Eclo.Persistence.Dtos.Users;
using Eclo.Persistence.Validations.Users;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.User.Users;

[Route("api/user/profile")]
[ApiController]
public class UserController : UserBaseController
{
    private readonly IIdentityService _identity;
    private readonly IUserService _service;

    public UserController(IUserService service,
         IIdentityService identity)
    {
        this._identity = identity;
        this._service = service;
    }

    [HttpGet("userId")]
    public async Task<IActionResult> GetByIdAsync()
        => Ok(await _service.GetByIdAsync(_identity.Id));

    [HttpPut("userId")]
    public async Task<IActionResult> UpdateAsync([FromForm] UserUpdateDto dto)
    {
        var updateValidator = new UserUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(_identity.Id, _identity.PhoneNumber, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpPut("phoneNumber")]
    public async Task<IActionResult> UpdatePhoneNumberAsync(string phoneNumber, [FromForm] UserUpdateDto dto)
    {
        var updateValidator = new UserUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdatePhoneNumberAsync(phoneNumber, dto));
        else return BadRequest(validationResult.Errors);

    }
}
