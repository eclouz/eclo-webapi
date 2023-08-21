using Eclo.Persistence.Dtos.Notifications;
using Eclo.Services.Interfaces.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers;

[Route("api/sms")]
[ApiController]
public class SmsController : ControllerBase
{
    private readonly ISmsSender _smsSender;
    public SmsController(ISmsSender smsSender)
    {
        this._smsSender = smsSender;
    }

    [HttpPost]
    public async Task<IActionResult> SendAsync([FromBody] SmsMessage smsMessage)
    {
        return Ok(await _smsSender.SendAsync(smsMessage));
    }
}
