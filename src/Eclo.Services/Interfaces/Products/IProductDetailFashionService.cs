using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;

namespace Eclo.Services.Interfaces.Products;

public interface IProductDetailFashionService
{
    public Task<bool> CreateAsync(ProductDetailFashionCreateDto dto);

    public Task<bool> DeleteAsync(long productDetailFashionId);

    public Task<long> CountAsync();

    public Task<IList<ProductDetailFashion>> GetAllAsync(PaginationParams @params);

    public Task<ProductDetailFashion> GetByIdAsync(long productDetailFashionId);

    public Task<bool> UpdateAsync(long productDetailFashionId, ProductDetailFashionUpdateDto dto);
}
