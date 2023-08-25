using Eclo.Application.Utilities;
using Eclo.Persistence.Dtos.Users;
using Eclo.Persistence.Validations.Users;
using Eclo.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.User.Users;

[Route("api/user/users")]
[ApiController]
public class UserController : UserBaseController
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByIdAsync(long userId)
        => Ok(await _service.GetByIdAsync(userId));

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateAsync(long userId, [FromForm] UserUpdateDto dto)
    {
        var updateValidator = new UserUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(userId, dto));
        else return BadRequest(validationResult.Errors);
    }

    
}
