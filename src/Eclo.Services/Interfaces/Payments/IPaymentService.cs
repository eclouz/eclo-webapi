using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Payments;

namespace Eclo.Services.Interfaces.Payments;

public interface IPaymentService
{
    public Task<bool> DeleteAsync(long paymentId);

    public Task<long> CountAsync();

    public Task<IList<Payment>> GetAllAsync(PaginationParams @params);

    public Task<Payment> GetByIdAsync(long paymentId);
}