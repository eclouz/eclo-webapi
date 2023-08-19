using Eclo.Application.Utilities;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Persistence.Dtos.Products;

namespace Eclo.Services.Interfaces.Products;

public interface IProductService
{
    public Task<bool> CreateAsync(ProductCreateDto dto);

    public Task<bool> DeleteAsync(long productId);

    public Task<long> CountAsync();

    public Task<IList<ProductViewModel>> GetAllAsync(PaginationParams @params);

    public Task<ProductViewModel> GetByIdAsync(long productId);

    public Task<bool> UpdateAsync(long productId, ProductUpdateDto dto);

    public Task<IList<ProductViewModel>> SearchAsync(string search, PaginationParams @params);
}
