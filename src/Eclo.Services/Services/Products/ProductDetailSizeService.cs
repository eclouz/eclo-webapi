using Eclo.Application.Exceptions.Products;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Products;

namespace Eclo.Services.Services.Products;

public class ProductDetailSizeService : IProductDetailSizeService
{
    private readonly IProductDetailSizeRepository _repository;
    private readonly IPaginator _paginator;

    public ProductDetailSizeService(IProductDetailSizeRepository productDetailSizeRepository,
        IPaginator paginator)
    {
        this._repository = productDetailSizeRepository;
        this._paginator = paginator;
    }
    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(ProductDetailSizeCreateDto dto)
    {
        ProductDetailSize productDetailSize = new ProductDetailSize()
        {
            ProductDetailId = dto.ProductDetailId,
            Size = dto.Size,
            Quantity = dto.Quantity,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(productDetailSize);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long productDetailSizeId)
    {
        var productDetailSize = await _repository.GetByIdAsync(productDetailSizeId);
        if (productDetailSize == null) throw new ProductNotFoundException();

        var dbResult = await _repository.DeleteAsync(productDetailSizeId);

        return dbResult > 0;
    }

    public async Task<IList<ProductDetailSize>> GetAllAsync(PaginationParams @params)
    {
        var productDetailSize = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return productDetailSize;
    }

    public async Task<IList<ProductDetailSize>> GetAllByProductDetailIdAsync(long productDetailId)
    {
        var productDetailSizes = await _repository.GetAllByProductDetailIdAsync(productDetailId);
        return productDetailSizes;
    }

    public async Task<ProductDetailSize> GetByIdAsync(long productDetailSizeId)
    {
        var productDetailSize = await _repository.GetByIdAsync(productDetailSizeId);
        if (productDetailSize == null) throw new ProductNotFoundException();
        else return productDetailSize;
    }

    public async Task<bool> UpdateAsync(long productDetailSizeId, ProductDetailSizeUpdateDto dto)
    {
        var productDetailSize = await _repository.GetByIdAsync(productDetailSizeId);
        if (productDetailSize == null) throw new ProductNotFoundException();

        // update Product Detail Size with new items
        productDetailSize.ProductDetailId = dto.ProductDetailId;
        productDetailSize.Size = dto.Size;
        productDetailSize.Quantity = dto.Quantity;
        productDetailSize.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(productDetailSizeId, productDetailSize);

        return dbResult > 0;
    }
}
