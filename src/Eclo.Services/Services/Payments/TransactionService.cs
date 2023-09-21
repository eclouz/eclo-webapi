using Eclo.Application.Exceptions.Users;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Payments;
using Eclo.Persistence.Dtos.Payments;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Interfaces.Payments;

namespace Eclo.Services.Services.Payments;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IIdentityService _identity;
    private readonly ICardRepository _cardRepository;
    private readonly string receiverCardNumber = "8600 3129 1066 2275";

    public TransactionService(ITransactionRepository transactionRepository,
        IIdentityService identity,
        ICardRepository cardRepository)
    {
        this._transactionRepository = transactionRepository;
        this._identity = identity;
        this._cardRepository = cardRepository;
    }

    /*public async Task<bool> CreateCardAsync(CardCreateDto cDto)
    {
        Card card = new Card()
        {
            UserId = _identity.Id,
            CardHolderName = cDto.CardHolderName,
            CardNumber = cDto.CardNumber,
            PinCode = cDto.PinCode,
            Balance = cDto.Balance,
            ExpiredMonth = cDto.ExpiredMonth,
            ExpiredYear = cDto.ExpiredYear,
            IsActive = cDto.IsActive,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var cardResult = await _cardRepository.CreateAsync(card);

        return cardResult > 0;
    }*/
    public async Task<bool> CreateTransactionAsync(TransactionCreateDto tDto, PaginationParams @params)
    {
        /*Transaction transaction = new Transaction()
        {
            UserId = _identity.Id,
            SenderCardNumber = tDto.SenderCardNumber,
            ReceiverCardNumber = receiverCardNumber,
            RequiredAmount = tDto.RequiredAmount,
            IsTransfered = true,
            Status = "Transfered",
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };*/

        //var transResult = await _transactionRepository.CreateAsync(transaction);

        var users = await _cardRepository.GetAllAsync(@params);

        foreach (var item in users)
        {
            if (item.CardNumber.ToString() == tDto.SenderCardNumber)
            {
                var user = await _cardRepository.GetByIdAsync(item.Id);
                if (item.UserId == _identity.Id && item.CardNumber.ToString() == tDto.SenderCardNumber)
                {
                    if (user == null) throw new UserNotFoundException();
                    if (user.Balance < 0 || user.Balance < tDto.RequiredAmount) return false;
                    else
                    {
                        var result = await _transactionRepository.TransactionProcedure(tDto.SenderCardNumber, receiverCardNumber, tDto.RequiredAmount);
                        if (result > 0)
                            return true;
                        return false;
                    }
                }
            }
        }
        return false;
    }
}
