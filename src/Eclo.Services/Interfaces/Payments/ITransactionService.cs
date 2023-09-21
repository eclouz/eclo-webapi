using Eclo.Application.Utilities;
using Eclo.Persistence.Dtos.Payments;

namespace Eclo.Services.Interfaces.Payments;

public interface ITransactionService
{
    public Task<bool> CreateTransactionAsync(TransactionCreateDto tDto, PaginationParams @params);
}
