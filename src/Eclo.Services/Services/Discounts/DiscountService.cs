using AutoMapper;
using Eclo.Application.Exceptions.Discounts;
using Eclo.Application.Utilities;
using Eclo.DataAccess.Interfaces.Discounts;
using Eclo.Domain.Entities.Discounts;
using Eclo.Persistence.Dtos.Discounts;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Common;
using Eclo.Services.Interfaces.Discounts;

namespace Eclo.Services.Services.Discounts;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _repository;
    private readonly IPaginator _paginator;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository,
        IPaginator paginator,
        IMapper mapper)
    {
        this._repository = discountRepository;
        this._paginator = paginator;
        this._mapper = mapper;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(DiscountCreateDto dto)
    {
        Discount discount = _mapper.Map<Discount>(dto);
        discount.CreatedAt = discount.UpdatedAt = TimeHelper.GetDateTime();

        var result = await _repository.CreateAsync(discount);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long discountId)
    {
        var discount = await _repository.GetByIdAsync(discountId);
        if (discount == null) throw new DiscountNotFoundException();

        var dbResult = await _repository.DeleteAsync(discountId);

        return dbResult > 0;
    }

    public async Task<IList<Discount>> GetAllAsync(PaginationParams @params)
    {
        var discounts = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return discounts;
    }

    public async Task<Discount> GetByIdAsync(long discountId)
    {
        var discount = await _repository.GetByIdAsync(discountId);
        if (discount == null) throw new DiscountNotFoundException();
        else return discount;
    }

    public async Task<bool> UpdateAsync(long discountId, DiscountUpdateDto dto)
    {
        var discount = await _repository.GetByIdAsync(discountId);
        if (discount == null) throw new DiscountNotFoundException();

        // update discount with new items
        discount.Name = dto.Name;
        discount.Percentage = dto.Percentage;
        discount.Description = dto.Description;
        discount.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(discountId, discount);

        return dbResult > 0;
    }
}
