using Eclo.Application.Utilities;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Persistence.Dtos.Products;

namespace Eclo.Services.Interfaces.Products;

public interface IProductDetailService
{
    public Task<bool> CreateAsync(ProductDetailCreateDto dto);

    public Task<bool> DeleteAsync(long productDetailId);

    public Task<long> CountAsync();

    public Task<IList<ProductViewModel>> GetAllAsync(PaginationParams @params);

    public Task<ProductViewModel> GetByIdAsync(long productDetailId);

    public Task<bool> UpdateAsync(long productDetailId, ProductDetailUpdateDto dto);

    public Task<IList<ProductViewModel>> SearchAsync(string search, PaginationParams @params);
}
