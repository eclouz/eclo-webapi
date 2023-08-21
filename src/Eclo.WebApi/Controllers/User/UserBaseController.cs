using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.User;

[ApiController]
//[Authorize(Roles = "User")]
[AllowAnonymous]
public abstract class UserBaseController : ControllerBase
{
}
