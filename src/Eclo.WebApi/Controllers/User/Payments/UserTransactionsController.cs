using Eclo.Application.Utilities;
using Eclo.Persistence.Dtos.Payments;
using Eclo.Persistence.Validations.Payments;
using Eclo.Services.Interfaces.Payments;
using Microsoft.AspNetCore.Mvc;

namespace Eclo.WebApi.Controllers.User.Payments;

[Route("api/user/transactions")]
[ApiController]
public class UserTransactionsController : UserBaseController
{
    private readonly ITransactionService _trans;
    private readonly int maxPageSize = 30;

    public UserTransactionsController(ITransactionService transaction)
    {
        this._trans = transaction;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync([FromForm] TransactionCreateDto transactionCreateDto)
    {
        var validator = new TransactionCreateValidator();
        var result = validator.Validate(transactionCreateDto);
        if (result.IsValid) return Ok(await _trans.CreateTransactionAsync(transactionCreateDto, new PaginationParams(1, maxPageSize)));
        return Ok(result.Errors);
    }
}
