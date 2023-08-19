using Eclo.Application.Utilities;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;

namespace Eclo.Services.Interfaces.Products;

public interface IUserProductLikeService
{
    public Task<bool> CreateAsync(UserProductLikeCreateDto dto);

    public Task<bool> DeleteAsync(long userProductLikeId);

    public Task<long> CountAsync();

    public Task<IList<UserProductLike>> GetAllAsync(PaginationParams @params);

    public Task<UserProductLike> GetByIdAsync(long userProductLikeId);

    public Task<bool> UpdateAsync(long userProductLikeId, UserProductLikeUpdateDto dto);
}
