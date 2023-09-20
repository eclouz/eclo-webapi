using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Payments;

namespace Eclo.DataAccess.Interfaces.Payments;

public interface ICardRepository : IRepository<Card, Card>,
    IGetAll<Card>, ISearchable<Card>
{
}
