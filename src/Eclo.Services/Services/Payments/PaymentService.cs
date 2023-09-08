using Eclo.Application.Exceptions.Payments;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Payments;
using Eclo.Domain.Entities.Payments;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Payments;

namespace Eclo.Services.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaginator _paginator;

    public PaymentService(IPaymentRepository paymentRepository, IPaginator paginator)
    {
        this._paymentRepository = paymentRepository;
        this._paginator = paginator;
    }
    public async Task<long> CountAsync() => await _paymentRepository.CountAsync();

    public async Task<bool> DeleteAsync(long paymentId)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentId);
        if (payment == null) throw new PaymentNotFoundException();

        var dbResult = await _paymentRepository.DeleteAsync(paymentId);
        return dbResult > 0;
    }

    public async Task<IList<Payment>> GetAllAsync(PaginationParams @params)
    {
        var payments = await _paymentRepository.GetAllAsync(@params);
        var count = await _paymentRepository.CountAsync();
        _paginator.Paginate(count, @params);

        return payments;
    }

    public async Task<Payment> GetByIdAsync(long paymentId)
    {
        var payment = await _paymentRepository.GetByIdAsync(@paymentId);
        if (payment == null) throw new PaymentNotFoundException();
        else return payment;
    }
}