using Eclo.Persistence.Dtos.Heads;
using Eclo.Services.Interfaces.Heads;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Common.Heads;

[Route("api/common/heads")]
[ApiController]
public class CommonHeadController : ControllerBase
{
    private readonly IHeadService _service;

    public CommonHeadController(IHeadService head)
    {
        this._service = head;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] HeadCreateDto dto) => Ok(await _service.CreateAsync(dto));
}
