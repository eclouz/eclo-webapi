using Eclo.Application.Utilities;
using Eclo.Services.Interfaces.Payments;
using Eclo.Services.Services.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.Admin.Payments
{
    [Route("api/admin/cards")]
    [ApiController]
    public class AdminCardsController : AdminBaseController
    {
        private readonly ICardService _service;
        private readonly int maxPageSize = 30;

        public AdminCardsController(ICardService cardService)
        {
            this._service = cardService;
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync() => Ok(await _service.CountAsync());

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{cardId}")]
        public async Task<IActionResult> GetByIdAsync(long cardId)
            => Ok(await _service.GetByIdAsync(cardId));
    }
}
