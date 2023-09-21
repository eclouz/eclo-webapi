using Eclo.Application.Exceptions.Payments;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Payments;
using Eclo.Domain.Entities.Payments;
using Eclo.Persistence.Dtos.Payments;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Payments;

namespace Eclo.Services.Services.Payments;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;
    private readonly IPaginator _paginator;
    private readonly IIdentityService _identity;

    public CardService(ICardRepository cardRepository,
        IPaginator paginator,
        IIdentityService identity)
    {
        this._cardRepository = cardRepository;
        this._paginator = paginator;
        this._identity = identity;
    }

    public async Task<long> CountAsync() => await _cardRepository.CountAsync();

    public async Task<bool> CreateAsync(CardCreateDto dto)
    {
        Card card = new Card()
        {
            UserId = _identity.Id,
            CardHolderName = dto.CardHolderName,
            CardNumber = dto.CardNumber,
            PinCode = dto.PinCode,
            Balance = dto.Balance,
            ExpiredMonth = dto.ExpiredMonth,
            ExpiredYear = dto.ExpiredYear,
            IsActive = dto.IsActive,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _cardRepository.CreateAsync(card);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long cardId)
    {
        var card = await _cardRepository.GetByIdAsync(cardId);
        if (card == null) throw new CardNotFoundException();

        var dbResult = await _cardRepository.DeleteAsync(cardId);
        return dbResult > 0;
    }

    public async Task<IList<Card>> GetAllAsync(PaginationParams @params)
    {
        var cards = await _cardRepository.GetAllAsync(@params);
        var count = await _cardRepository.CountAsync();
        _paginator.Paginate(count, @params);

        return cards;
    }

    public async Task<Card> GetByIdAsync(long cardId)
    {
        var card = await _cardRepository.GetByIdAsync(cardId);
        if (card == null) throw new CardNotFoundException();
        return card;
    }

    public async Task<bool> UpdateAsync(long cardId, CardUpdateDto dto)
    {
        var card = await _cardRepository.GetByIdAsync(cardId);
        if (card == null) throw new CardNotFoundException();

        card.UserId = dto.UserId;
        card.CardHolderName = dto.CardHolderName;
        card.CardNumber = dto.CardNumber;
        card.Balance = dto.Balance;
        card.PinCode = dto.PinCode;
        card.ExpiredMonth = dto.ExpiredMonth;
        card.ExpiredYear = dto.ExpiredYear;
        card.IsActive = dto.IsActive;
        card.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _cardRepository.UpdateAsync(cardId, card);

        return dbResult > 0;
    }
}
