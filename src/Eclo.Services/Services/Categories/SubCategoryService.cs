using Eclo.Application.Exceptions.Categories;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Categories;
using Eclo.Domain.Entities.Categories;
using Eclo.Persistence.Dtos.Categories;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Categories;
using Eclo.Services.Interfaces.Common;

namespace Eclo.Services.Services.Categories;

public class SubCategoryService : ISubCategoryService
{
    private readonly ISubCategoryRepository _repository;
    private readonly IPaginator _paginator;

    public SubCategoryService(ISubCategoryRepository subCategoryRepository,
        IPaginator paginator)
    {
        this._repository = subCategoryRepository;
        this._paginator = paginator;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(SubCategoryCreateDto dto)
    {
        SubCategory subCategory = new SubCategory()
        {
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(subCategory);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long subCategoryId)
    {
        var subCategory = await _repository.GetByIdAsync(subCategoryId);
        if (subCategory == null) throw new CategoryNotFoundException();

        var dbResult = await _repository.DeleteAsync(subCategoryId);

        return dbResult > 0;
    }

    public async Task<IList<SubCategory>> GetAllAsync(PaginationParams @params)
    {
        var subCategories = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return subCategories;
    }

    public async Task<SubCategory> GetByIdAsync(long subCategoryId)
    {
        var subCategory = await _repository.GetByIdAsync(subCategoryId);
        if (subCategory == null) throw new CategoryNotFoundException();
        else return subCategory;
    }

    public async Task<bool> UpdateAsync(long subCategoryId, SubCategoryUpdateDto dto)
    {
        var subCategory = await _repository.GetByIdAsync(subCategoryId);
        if (subCategory == null) throw new CategoryNotFoundException();

        // update subcategory with new items
        subCategory.Name = dto.Name;
        subCategory.CategoryId = dto.CategoryId;
        subCategory.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(subCategoryId, subCategory);

        return dbResult > 0;
    }
}
