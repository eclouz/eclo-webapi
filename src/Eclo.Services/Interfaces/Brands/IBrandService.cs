using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Brands;
using Eclo.Persistence.Dtos.Brands;

namespace Eclo.Services.Interfaces.Brands;

public interface IBrandService
{
    public Task<bool> CreatAsync(BrandCreateDto dto);

    public Task<bool> DeleteAsync(long brandId);

    public Task<long> CountAsync();

    public Task<IList<Brand>> GetAllAsync(PaginationParams @params);

    public Task<Brand> GetByIdAsync(long brandId);

    public Task<bool> UpdateAsync(long brandId, BrandUpdateDto dto);
}
