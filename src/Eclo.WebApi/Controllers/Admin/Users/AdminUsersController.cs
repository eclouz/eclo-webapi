using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Users;

[Route("api/admin/users")]
[ApiController]
public class AdminUsersController : AdminBaseController
{
    private readonly IUserService _userService;
    private readonly IAdminUserService _service;
    private readonly int maxPageSize = 30;

    public AdminUsersController(IAdminUserService service,
        IUserService userService)
    {
        this._service = service;
        this._userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByIdAsync(long userId)
        => Ok(await _service.GetByIdAsync(userId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync(string search, [FromQuery] int page = 1)
        => Ok(await _service.SearchAsync(search, new PaginationParams(page, maxPageSize)));

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAsync(long userId)
        => Ok(await _service.DeleteAsync(userId));


    [HttpGet("userPhoneNumber")]
    public async Task<IActionResult> GetByPhoneNumberAsync(string userPhoneNumber)
        => Ok(await _userService.GetByPhoneAsync(userPhoneNumber, new PaginationParams(1, maxPageSize)));
}
