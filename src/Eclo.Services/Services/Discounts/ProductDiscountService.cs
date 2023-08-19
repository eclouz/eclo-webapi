using Eclo.Application.Exceptions.Products;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.Domain.Entities.Discounts;
using Eclo.Persistence.Dtos.Discounts;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Discounts;

namespace Eclo.Services.Services.Discounts;

public class ProductDiscountService : IProductDiscountService
{
    private readonly IProductDiscountRepository _repository;
    private readonly IPaginator _paginator;

    public ProductDiscountService(IProductDiscountRepository productDiscountRepository,
        IPaginator paginator)
    {
        this._repository = productDiscountRepository;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(ProductDiscountCreateDto dto)
    {
        ProductDiscount productDiscount = new ProductDiscount()
        {
            ProductId = dto.ProductId,
            DiscountId = dto.DiscountId,
            Description = dto.Description,
            StartAt = dto.StartAt,
            EndAt = dto.EndAt,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(productDiscount);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long productDiscountId)
    {
        var productDiscount = await _repository.GetByIdAsync(productDiscountId);
        if (productDiscount == null) throw new ProductDiscountNotFoundException();

        var dbResult = await _repository.DeleteAsync(productDiscountId);

        return dbResult > 0;
    }

    public async Task<IList<ProductDiscount>> GetAllAsync(PaginationParams @params)
    {
        var productDiscounts = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return productDiscounts;
    }

    public async Task<ProductDiscount> GetByIdAsync(long productDiscountId)
    {
        var productDiscount = await _repository.GetByIdAsync(productDiscountId);
        if (productDiscount == null) throw new ProductDiscountNotFoundException();
        else return productDiscount;
    }

    public async Task<bool> UpdateAsync(long productDiscountId, ProductDiscountUpdateDto dto)
    {
        var productDiscount = await _repository.GetByIdAsync(productDiscountId);
        if (productDiscount == null) throw new ProductDiscountNotFoundException();

        // update product discount with new items
        productDiscount.ProductId = dto.ProductId;
        productDiscount.DiscountId = dto.DiscountId;
        productDiscount.Description = dto.Description;
        productDiscount.StartAt = dto.StartAt;
        productDiscount.EndAt = dto.EndAt;
        productDiscount.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(productDiscountId, productDiscount);

        return dbResult > 0;
    }
}
