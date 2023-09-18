using Eclo.Application.Utilities;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;

namespace Eclo.Services.Interfaces.Products;

public interface IProductService
{
    public Task<bool> CreateAsync(ProductCreateDto dto);

    public Task<bool> DeleteAsync(long productId);

    public Task<long> CountAsync();

    public Task<IList<Product>> GetAllAsync(PaginationParams @params);

    public Task<Product> GetByIdAsync(long productId);

    public Task<bool> UpdateAsync(long productId, ProductUpdateDto dto);

    public Task<IList<ProductViewModel>> GetAllViewAsync(PaginationParams @params);
    public Task<IList<ProductViewModel>> GetAllUserIdViewAsync(long userId, PaginationParams @params);

    public Task<IList<ProductGetViewModel>> GetAllView(PaginationParams @params);
    public Task<IList<ProductResultViewModel>> GetAllProductsView(PaginationParams @params);
    public Task<IList<ProductGetViewModels>> GetAllUserIdView(long userId, PaginationParams @params);

    public Task<ProductGetViewModel> GetByIdViewAsync(long productId, PaginationParams @params);
    public Task<ProductGetViewModel> GetByIdUserViewAsync(long userId, long productId, PaginationParams @params);

    public Task<IList<ProductGetViewModel>> FiltrAsync(string category, int min, int max, List<string> subCategories, PaginationParams @params);
    public Task<IList<ProductGetViewModels>> FiltrUserIdAsync(long userId, string category, int min, int max, List<string> subCategories, PaginationParams @params);

    public Task<IList<Product>> SearchAsync(string search, PaginationParams @params);
}
