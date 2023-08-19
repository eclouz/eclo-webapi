using Eclo.Application.Exceptions.Files;
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

public class ProductDetailFashionService : IProductDetailFashionService
{
    private readonly IProductDetailFashionRepository _repository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;

    public ProductDetailFashionService(IProductDetailFashionRepository repository,
        IFileService fileService,
        IPaginator paginator)
    {
        this._repository = repository;
        this._fileService = fileService;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(ProductDetailFashionCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.ImagePath);

        ProductDetailFashion productDetailFashion = new ProductDetailFashion()
        {
            ProductDetailId = dto.ProductDetailId,
            ImagePath = imagepath,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(productDetailFashion);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long productDetailFashionId)
    {
        var productDetailFashion = await _repository.GetById(productDetailFashionId);
        if (productDetailFashion == null) throw new ProductNotFoundException();

        var result = await _fileService.DeleteImageAsync(productDetailFashion.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _repository.DeleteAsync(productDetailFashionId);

        return dbResult > 0;
    }

    public async Task<IList<ProductDetailFashionViewModel>> GetAllAsync(PaginationParams @params)
    {
        var productDetailFashions = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return productDetailFashions;
    }

    public async Task<ProductDetailFashionViewModel> GetByIdAsync(long productDetailFashionId)
    {
        var productDetailFashion = await _repository.GetByIdAsync(productDetailFashionId);
        if (productDetailFashion == null) throw new ProductNotFoundException();
        else return productDetailFashion;
    }

    public async Task<bool> UpdateAsync(long productDetailFashionId, ProductDetailFashionUpdateDto dto)
    {
        var productDetailFashion = await _repository.GetById(productDetailFashionId);
        if (productDetailFashion == null) throw new ProductNotFoundException();

        // update productDetailFashion with new items 
        productDetailFashion.ProductDetailId = dto.ProductDetailId;

        if (dto.ImagePath is not null)
        {
            // delete old image
            var deleteResult = await _fileService.DeleteImageAsync(productDetailFashion.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            // upload new image
            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            // parse new path to productDetailFashion
            productDetailFashion.ImagePath = newImagePath;
        }
        // else productDetailFashion old image is have to save

        productDetailFashion.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(productDetailFashionId, productDetailFashion);

        return dbResult > 0;
    }
}
