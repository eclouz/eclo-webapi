using Eclo.Application.Exceptions.Products;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Products;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Products;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Products;

namespace Eclo.Services.Services.Products;

public class UserProductLikeService : IUserProductLikeService
{
    private readonly IUserProductLikeRepository _repository;
    private readonly IPaginator _paginator;

    public UserProductLikeService(IUserProductLikeRepository userProductLikeRepository,
        IPaginator paginator)
    {
        this._repository = userProductLikeRepository;
        this._paginator = paginator;
    }
    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(UserProductLikeCreateDto dto)
    {
        UserProductLike userProductLike = new UserProductLike()
        {
            UserId = dto.UserId,
            ProductId = dto.ProductId,
            IsLiked = dto.IsLiked,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(userProductLike);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long userProductLikeId)
    {
        var userProductLike = await _repository.GetByIdAsync(userProductLikeId);
        if (userProductLike == null) throw new ProductNotFoundException();

        var dbResult = await _repository.DeleteAsync(userProductLikeId);

        return dbResult > 0;
    }

    public async Task<IList<UserProductLike>> GetAllAsync(PaginationParams @params)
    {
        var userProductLike = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return userProductLike;
    }

    public async Task<UserProductLike> GetByIdAsync(long userProductLikeId)
    {
        var userProductLike = await _repository.GetByIdAsync(userProductLikeId);
        if (userProductLike == null) throw new ProductNotFoundException();
        else return userProductLike;
    }

    public async Task<bool> UpdateAsync(long userProductLikeId, UserProductLikeUpdateDto dto)
    {
        var userProductLike = await _repository.GetByIdAsync(userProductLikeId);
        if (userProductLike == null) throw new ProductNotFoundException();

        // update User Product Like with new items
        userProductLike.UserId = dto.UserId;
        userProductLike.ProductId = dto.ProductId;
        userProductLike.IsLiked = dto.IsLiked;
        userProductLike.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(userProductLikeId, userProductLike);

        return dbResult > 0;
    }
}
