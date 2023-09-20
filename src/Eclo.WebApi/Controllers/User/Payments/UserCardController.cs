using Eclo.Persistence.Dtos.Orders;
using Eclo.Persistence.Dtos.Payments;
using Eclo.Persistence.Validations.Payments;
using Eclo.Services.Interfaces.Payments;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.User.Payments;

[Route("api/user/cards")]
[ApiController]
public class UserCardController : UserBaseController
{
    private readonly ICardService _cardService;
    private readonly int maxPageSize = 30;

    public UserCardController(ICardService card)
    {
        this._cardService = card;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CardCreateDto dto)
    {
        var validator = new CardCreateValidator();
        var result = await validator.ValidateAsync(dto);
        if (result.IsValid) return Ok(await _cardService.CreateAsync(dto));
        return BadRequest(result.Errors);
    }

    [HttpDelete("{cardId}")]
    public async Task<IActionResult> DeleteAsync(long cardId) 
        => Ok(await _cardService.DeleteAsync(cardId));

    [HttpPut("{cardId}")]
    public async Task<IActionResult> UpdateAsync(long cardId, [FromForm] CardUpdateDto dto)
    {
        var validator = new CardUpdateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _cardService.UpdateAsync(cardId, dto));
        return BadRequest(result.Errors);
    }
}
