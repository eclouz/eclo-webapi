using Eclo.Application.Exceptions.Brands;
using Eclo.Application.Exceptions.Files;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Brands;
using Eclo.Domain.Entities.Brands;
using Eclo.Persistence.Dtos.Brands;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Brands;
using Eclo.Services.Interfaces.Common;

namespace Eclo.Services.Services.Brands;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _repository;
    private readonly IFileService _fileService;
    private readonly IPaginator _paginator;

    public BrandService(IBrandRepository brandRepository,
        IFileService fileService,
        IPaginator paginator)
    {
        this._repository = brandRepository;
        this._fileService = fileService;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreatAsync(BrandCreateDto dto)
    {
        string iconpath = await _fileService.UploadIconAsync(dto.BrandIconPath);
        
        Brand brand = new Brand()
        {
            BrandIconPath = iconpath,
            Name = dto.Name,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(brand);
        
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long brandId)
    {
        var brand = await _repository.GetByIdAsync(brandId);
        if (brand == null) throw new BrandNotFoundException();

        var result = await _fileService.DeleteIconAsync(brand.BrandIconPath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _repository.DeleteAsync(brandId);
        
        return dbResult > 0;
    }

    public async Task<IList<Brand>> GetAllAsync(PaginationParams @params)
    {
        var brands = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return brands;
    }

    public async Task<Brand> GetByIdAsync(long brandId)
    {
        var brand = await _repository.GetByIdAsync(brandId);
        if (brand is null) throw new BrandNotFoundException();
        else return brand;
    }

    public async Task<bool> UpdateAsync(long brandId, BrandUpdateDto dto)
    {
        var brand = await _repository.GetByIdAsync(brandId);
        if (brand is null) throw new BrandNotFoundException();

        // update brand with new items
        brand.Name = dto.Name;

        if (dto.BrandIconPath is not null)
        {
            // delete old icon
            var deleteResult = await _fileService.DeleteIconAsync(brand.BrandIconPath);
            if (deleteResult is false) throw new ImageNotFoundException();

            // upload new icon
            string newIconPath = await _fileService.UploadIconAsync(dto.BrandIconPath);

            // parse new path to brand
            brand.BrandIconPath = newIconPath;
        }
        // else brand old icon is have to save

        brand.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(brandId, brand);

        return dbResult > 0;
    }
}
