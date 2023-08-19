using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;

namespace Eclo.Services.Interfaces.Products;

public interface IProductDetailSizeService
{
    public Task<bool> CreateAsync(ProductDetailSizeCreateDto dto);

    public Task<bool> DeleteAsync(long productDetailSizeId);

    public Task<long> CountAsync();

    public Task<IList<ProductDetailSize>> GetAllAsync(PaginationParams @params);

    public Task<ProductDetailSize> GetByIdAsync(long productDetailSizeId);

    public Task<bool> UpdateAsync(long productDetailSizeId, ProductDetailSizeUpdateDto dto);
}
