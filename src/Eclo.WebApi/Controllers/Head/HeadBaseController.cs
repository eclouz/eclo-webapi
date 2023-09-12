using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Head;

[ApiController]
[Authorize(Roles = "Head")]
//[AllowAnonymous]
public class HeadBaseController : ControllerBase
{
}
