using Eclo.Persistence.Dtos.Discounts;
using FluentValidation;

namespace Eclo.Persistence.Validations.Discounts;

public class ProductDiscountCreateValidator : AbstractValidator<ProductDiscountCreateDto>
{
    public ProductDiscountCreateValidator()
    {
        RuleFor(dto => dto.Description).NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(3).WithMessage("Description field is required!");
    }
}
