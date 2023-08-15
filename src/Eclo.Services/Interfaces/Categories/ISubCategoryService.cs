using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Categories;
using Eclo.Persistence.Dtos.Categories;

namespace Eclo.Services.Interfaces.Categories;

public interface ISubCategoryService
{
    public Task<bool> CreateAsync(SubCategoryCreateDto dto);

    public Task<bool> DeleteAsync(long subCategoryId);

    public Task<long> CountAsync();

    public Task<IList<SubCategory>> GetAllAsync(PaginationParams @params);

    public Task<SubCategory> GetByIdAsync(long subCategoryId);

    public Task<bool> UpdateAsync(long subCategoryId, SubCategoryUpdateDto dto);
}
