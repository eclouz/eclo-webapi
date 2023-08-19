using Eclo.Application.Utilities;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Persistence.Dtos.Products;

namespace Eclo.Services.Interfaces.Products;

public interface IProductDetailFashionService
{
    public Task<bool> CreateAsync(ProductDetailFashionCreateDto dto);

    public Task<bool> DeleteAsync(long productDetailFashionId);

    public Task<long> CountAsync();

    public Task<IList<ProductDetailFashionViewModel>> GetAllAsync(PaginationParams @params);

    public Task<ProductDetailFashionViewModel> GetByIdAsync(long productDetailFashionId);

    public Task<bool> UpdateAsync(long productDetailFashionId, ProductDetailFashionUpdateDto dto);
}
