using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Discounts;
using Eclo.Persistence.Dtos.Discounts;

namespace Eclo.Services.Interfaces.Discounts;

public interface IDiscountService
{
    public Task<bool> CreateAsync(DiscountCreateDto dto);

    public Task<bool> DeleteAsync(long discountId);

    public Task<long> CountAsync();

    public Task<IList<Discount>> GetAllAsync(PaginationParams @params);

    public Task<Discount> GetByIdAsync(long discountId);

    public Task<bool> UpdateAsync(long discountId, DiscountUpdateDto dto);
}
