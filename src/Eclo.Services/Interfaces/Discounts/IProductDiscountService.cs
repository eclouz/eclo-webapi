using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Discounts;
using Eclo.Persistence.Dtos.Discounts;

namespace Eclo.Services.Interfaces.Discounts;

public interface IProductDiscountService
{
    public Task<bool> CreateAsync(ProductDiscountCreateDto dto);

    public Task<bool> DeleteAsync(long productDiscountId);

    public Task<long> CountAsync();

    public Task<IList<ProductDiscount>> GetAllAsync(PaginationParams @params);

    public Task<ProductDiscount> GetByIdAsync(long productDiscountId);

    public Task<bool> UpdateAsync(long productDiscountId, ProductDiscountUpdateDto dto);
}
