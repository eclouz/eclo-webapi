using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin, Head")]
//[AllowAnonymous]
public abstract class AdminBaseController : ControllerBase
{
}
