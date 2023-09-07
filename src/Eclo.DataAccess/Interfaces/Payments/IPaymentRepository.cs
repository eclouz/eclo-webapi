using Eclo.DataAccess.Common;
using Eclo.Domain.Entities.Payments;

namespace Eclo.DataAccess.Interfaces.Payments;

public interface IPaymentRepository : IRepository<Payment, Payment>,
    IGetAll<Payment>, ISearchable<Payment>
{
}