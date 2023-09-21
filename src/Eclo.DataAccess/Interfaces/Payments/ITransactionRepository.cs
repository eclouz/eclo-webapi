using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Payments;

namespace Eclo.DataAccess.Interfaces.Payments;

public interface ITransactionRepository : IRepository<Transaction, Transaction>,
    IGetAll<Transaction>
{
    public Task<int> TransactionProcedure(string senderCardNumber, string receiverCardNumber, double requiredAmount);
}
