using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Payments;
using Eclo.Persistence.Dtos.Payments;

namespace Eclo.Services.Interfaces.Payments;

public interface ICardService
{
    public Task<bool> CreateAsync(CardCreateDto dto);

    public Task<bool> DeleteAsync(long cardId);

    public Task<long> CountAsync();

    public Task<IList<Card>> GetAllAsync(PaginationParams @params);

    public Task<Card> GetByIdAsync(long cardId);

    public Task<bool> UpdateAsync(long cardId, CardUpdateDto dto);
}
