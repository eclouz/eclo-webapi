﻿using Eclo.Application.Exceptions.Files;
using Eclo.Application.Exceptions.Products;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Products;

namespace Eclo.Services.Services.Products;

public class ProductDetailService : IProductDetailService
{
    private readonly IProductDetailRepository _repository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;

    public ProductDetailService(IProductDetailRepository repository,
        IFileService fileService,
        IPaginator paginator)
    {
        this._repository = repository;
        this._fileService = fileService;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(ProductDetailCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.ImagePath);

        ProductDetail productDetail = new ProductDetail()
        {
            ProductId = dto.ProductId,
            Color = dto.Color,
            ImagePath = imagepath,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(productDetail);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long productDetailId)
    {
        var productDetail = await _repository.GetById(productDetailId);
        if (productDetail == null) throw new ProductNotFoundException();

        var result = await _fileService.DeleteImageAsync(productDetail.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _repository.DeleteAsync(productDetailId);

        return dbResult > 0;
    }

    public async Task<IList<ProductDetail>> GetAllAsync(PaginationParams @params)
    {
        var productDetails = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return productDetails;
    }

    public async Task<ProductDetail> GetByIdAsync(long productDetailId)
    {
        var productDetail = await _repository.GetByIdAsync(productDetailId);
        if (productDetail == null) throw new ProductNotFoundException();
        else return productDetail;
    }

    public async Task<IList<ProductDetail>> SearchAsync(string search, PaginationParams @params)
    {
        var productDetails = await _repository.SearchAsync(search, @params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return productDetails.Item2.ToList();
    }

    public async Task<bool> UpdateAsync(long productDetailId, ProductDetailUpdateDto dto)
    {
        var productDetail = await _repository.GetById(productDetailId);
        if (productDetail == null) throw new ProductNotFoundException();

        // update productDetail with new items 
        productDetail.ProductId = dto.ProductId;
        productDetail.Color = dto.Color;

        if (dto.ImagePath is not null)
        {
            // delete old image
            var deleteResult = await _fileService.DeleteImageAsync(productDetail.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            // upload new image
            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            // parse new path to productDetail
            productDetail.ImagePath = newImagePath;
        }
        // else productDetail old image is have to save

        productDetail.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(productDetailId, productDetail);

        return dbResult > 0;
    }
}
