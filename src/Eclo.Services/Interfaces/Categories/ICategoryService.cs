using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Categories;
using Eclo.Persistence.Dtos.Categories;

namespace Eclo.Services.Interfaces.Categories;

public interface ICategoryService
{
    public Task<bool> CreateAsync(CategoryCreateDto dto);

    public Task<bool> DeleteAsync(long categoryId);

    public Task<long> CountAsync();

    public Task<IList<Category>> GetAllAsync(PaginationParams @params);

    public Task<Category> GetByIdAsync(long cateogoryId);

    public Task<bool> UpdateAsync(long cateogoryId, CategoryUpdateDto dto);
}
