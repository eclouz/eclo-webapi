using Eclo.Application.Exceptions.Products;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.DataAccess.ViewModels.Products;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Products;

namespace Eclo.Services.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IPaginator _paginator;

    public ProductService(IProductRepository productRepository,
        IPaginator paginator)
    {
        this._repository = productRepository;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(ProductCreateDto dto)
    {
        Product product = new Product()
        {
            BrandId = dto.BrandId,
            SubCategoryId = dto.SubCategoryId,
            Name = dto.Name,
            UnitPrice = dto.UnitPrice,
            Description = dto.Description,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(product);
        
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long productId)
    {
        var product = await _repository.GetById(productId);
        if (product == null) throw new ProductNotFoundException();

        var dbResult = await _repository.DeleteAsync(productId);

        return dbResult > 0;
    }

    public async Task<IList<ProductViewModel>> GetAllAsync(PaginationParams @params)
    {
        var products = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return products;
    }

    public async Task<ProductViewModel> GetByIdAsync(long productId)
    {
        var product = await _repository.GetByIdAsync(productId);
        if (product == null) throw new ProductNotFoundException();
        else return product; 
    }

    public async Task<IList<ProductViewModel>> SearchAsync(string search, PaginationParams @params)
    {
        var products = await _repository.SearchAsync(search, @params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return products.Item2.ToList(); 
    }

    public async Task<bool> UpdateAsync(long productId, ProductUpdateDto dto)
    {
        var product = await _repository.GetById(productId);
        if (product == null) throw new ProductNotFoundException();

        // update product with new items 
        product.BrandId = dto.BrandId;
        product.SubCategoryId = dto.SubCategoryId;
        product.Name = dto.Name;
        product.UnitPrice = dto.UnitPrice;
        product.Description = dto.Description;
        product.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(productId, product);

        return dbResult > 0;
    }
}
