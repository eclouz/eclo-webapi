using Eclo.Persistence.Dtos.Discounts;
using FluentValidation;

namespace Eclo.Persistence.Validations.Discounts;

public class ProductDiscountUpdateValidator : AbstractValidator<ProductDiscountUpdateDto>
{
    public ProductDiscountUpdateValidator()
    {
        RuleFor(dto => dto.Description).NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(10).WithMessage("Description field is required!");
    }
}
